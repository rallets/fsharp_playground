import React, { FC, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import { ItemEditData, ItemEditForm } from './ItemEditForm';
import { createItem, editItem, useItem } from './itemHelpers';
import { ItemHeader } from './models';

export type ItemPageProps = {
	isNew: boolean;
	onEdited: (item: ItemHeader) => void;
	onAdded: (item: ItemEditData) => void;
	onCancel: () => void;
};

export type ItemPageParams = {
	id: string;
};

export const ItemPage: FC<ItemPageProps> = ({ isNew, onEdited, onAdded, onCancel }) => {
	const { id } = useParams<ItemPageParams>();
	// const [{ isValid, payload: item, loading }, doRefresh] = useItem(id);

	const [isEditing, setIsEditing] = useState(false);
	const [isAdding, setIsAdding] = useState(false);

	useEffect(() => {
		setIsEditing(false);
		if (!!id) {
			setIsAdding(false);
		}
	}, [id]);

	useEffect(() => {
		setIsAdding(isNew);
	}, [isNew]);

	function handleClose(): void {
		setIsEditing(false);
		setIsAdding(false);
		onCancel();
	}

	async function handleSave(item: ItemEditData): Promise<void> {
		const result = await editItem(id, item);
		if (result) {
			onEdited({ id, name: item.name, numTags: item.tags?.length || 0 });
			// doRefresh();
			setIsEditing(false);
			toast.info(`Item saved`);
		}
	}

	async function handleAdd(item: ItemEditData): Promise<void> {
		const result = await createItem(item);
		if (result) {
			onAdded(item);
			setIsAdding(false);
			toast.info(`Item created`);
		}
	}

	if (isAdding) {
		return <ItemEditForm item={null} handleClose={handleClose} handleSave={handleAdd} />;
	}

	return (
		<>
			{id && (
				<ItemDetailPage
					id={id}
					isEditing={isEditing}
					setIsEditing={setIsEditing}
					handleClose={handleClose}
					handleSave={handleSave}
				/>
			)}

			{/* <span>Item Page ID: {id}</span>
			<ObjectDump value={item} /> */}
		</>
	);
};

export type ItemDetailPageProps = {
	id: string;
	isEditing: boolean;
	setIsEditing: (value: boolean) => void;
	handleSave: (item: ItemEditData) => void;
	handleClose: () => void;
};

const ItemDetailPage: FC<ItemDetailPageProps> = ({ id, isEditing, setIsEditing, handleSave, handleClose }) => {
	const [{ isValid, payload: item, loading }, doRefresh] = useItem(id);

	if (!isValid || !item) {
		return <p>Invalid item</p>;
	}

	return (
		<>
			{loading && <p>Loading...</p>}

			{isEditing && (
				<ItemEditForm
					item={item}
					handleClose={handleClose}
					handleSave={async (item) => {
						await handleSave(item);
						doRefresh();
					}}
				/>
			)}

			{!isEditing && (
				<>
					<div className="row mb-2">
						<div className="col">
							<button className="btn btn-outline-primary float-right" onClick={() => setIsEditing(true)}>
								Edit
							</button>
						</div>
					</div>
					<div className="row">
						<div className="col-4">Id</div>
						<div className="col">{item?.id}</div>
					</div>
					<div className="row">
						<div className="col-4">Name</div>
						<div className="col">{item?.name}</div>
					</div>
					<div className="row">
						<div className="col-4">Description</div>
						<div className="col">{item?.description}</div>
					</div>
					<div className="row">
						<div className="col-4">Tags</div>
						<div className="col">
							{!item?.tags.length && <span>---</span>}
							{item?.tags?.map((tag) => (
								<span key={tag.id} className="badge badge-pill badge-info">
									{tag.name}
								</span>
							))}
						</div>
					</div>
				</>
			)}
			{/* <span>Item Page ID: {id}</span>
			<ObjectDump value={item} /> */}
		</>
	);
};
