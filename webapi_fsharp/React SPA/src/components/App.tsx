import React, { FC } from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { Header } from './common/Header';
import { NotFoundPage } from './common/NotFoundPage';
import { HomePage } from './HomePage';
import { ItemsPage } from './items/ItemsPage';

const App: FC = () => {
	return (
		<>
			<ToastContainer autoClose={3000} hideProgressBar />

			<BrowserRouter>
				<Header />

				<Switch>
					<Route path="/" exact component={HomePage} />
					<Route path="/items" component={ItemsPage} />
					<Route path="*" component={NotFoundPage} />
				</Switch>
			</BrowserRouter>
		</>
	);
};

export default App;
