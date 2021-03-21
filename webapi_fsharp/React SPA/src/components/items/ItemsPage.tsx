import React, { FC, useEffect, useState } from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';
import { guid } from '../../models/guid';
import { ItemEditData } from './ItemEditForm';
import { deleteItem, searchItems, useGetItems } from './itemHelpers';
import { ItemPage } from './ItemPage';
import ItemRow from './ItemRow';
import { ItemsSearchData, ItemsSearchForm } from './ItemsSearchForm';
import { ItemHeader } from './models';

export type ItemsPageProps = {};

export const ItemsPage: FC<ItemsPageProps> = () => {
	const [{ payload: apiItems, loading }, doRefresh] = useGetItems();
	const [updatedItem, setUpdatedItem] = useState<ItemHeader | null>(null);
	const [items, setItems] = useState<ItemHeader[]>([]);
	const [orderedItems, setOrderedItems] = useState<ItemHeader[]>([]);
	const [isAdding, setIsAdding] = useState<boolean>(false);

	useEffect(() => {
		setItems(orderItems(apiItems || []));
	}, [apiItems]);

	useEffect(() => {
		setOrderedItems(orderItems(items));
	}, [items]);

	useEffect(() => {
		if (!updatedItem) {
			return;
		}

		setItems((items) => {
			let updated = items.map((x) => (x.id === updatedItem.id ? updatedItem : x));
			updated = orderItems(updated);
			return updated;
		});

		setUpdatedItem(null);
	}, [updatedItem]);

	// The `path` lets us build <Route> paths that are
	// relative to the parent route, while the `url` lets
	// us build relative links.
	const { path } = useRouteMatch();

	function orderItems(items: ItemHeader[]): ItemHeader[] {
		return items.sort((a, b) => a.name.localeCompare(b.name));
	}

	function handleCancelEditing(): void {
		setIsAdding(false);
	}

	function handleEdited(item: ItemHeader): void {
		setUpdatedItem(item);
	}

	function handleAdded(_item: ItemEditData): void {
		doRefresh();
	}

	async function handleDeleteItem(id: guid): Promise<void> {
		const result = await deleteItem(id);
		if (result) {
			doRefresh();
		}
	}

	async function handleSearch(payload: ItemsSearchData): Promise<void> {
		const result = await searchItems(payload.type, payload.text);
		if (result) {
			setItems(result);
		}
	}

	async function handleResetSearch(): Promise<void> {
		doRefresh();
	}

	return (
		<>
			<div className="container-fluid">
				<div className="row">
					<div className="col-4">
						<div className="d-flex flex-row-reverse mb-2">
							<button
								className={`btn btn-outline-info align-self-end ${isAdding ? 'active' : ''}`}
								onClick={() => setIsAdding(!isAdding)}>
								Add
							</button>
						</div>

						<ItemsSearchForm handleSearch={handleSearch} handleReset={handleResetSearch} />

						<div className="list-group">
							{loading && <p>Loading...</p>}

							{orderedItems.length === 0 && <p>No items found</p>}

							{orderedItems.map((item) => (
								<ItemRow key={item.id} disabled={isAdding} item={item} onDeleteItem={handleDeleteItem} />
							))}
						</div>
					</div>

					<div className="col-8">
						{isAdding && <ItemPage isNew={true} onEdited={handleEdited} onAdded={handleAdded} onCancel={handleCancelEditing} />}
						{!isAdding && (
							<Switch>
								<Route path={`${path}/:id`}>
									<ItemPage isNew={false} onEdited={handleEdited} onAdded={handleAdded} onCancel={handleCancelEditing} />
								</Route>
								<Route path={path}>
									<h3>Please select a item.</h3>
								</Route>
							</Switch>
						)}
					</div>
				</div>
			</div>
		</>
	);
};
