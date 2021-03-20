import React, { FC } from 'react';
import { NavLink } from 'react-router-dom';

export const Header: FC = () => {
	return (
		<>
			<nav className="navbar navbar-expand-lg navbar-light bg-light mb-2">
				<span className="navbar-brand">React demo</span>

				<div className="collapse navbar-collapse" id="navbarSupportedContent">
					<ul className="navbar-nav mr-auto">
						<li className="nav-item">
							<NavLink className="nav-link" activeClassName="active" exact to="/">
								Home
							</NavLink>
						</li>
						<li className="nav-item">
							<NavLink className="nav-link" activeClassName="active" to="/items">
								Items
							</NavLink>
						</li>
					</ul>
				</div>
			</nav>
		</>
	);
};
