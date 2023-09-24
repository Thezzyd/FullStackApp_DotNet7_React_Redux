import {createBrowserRouter, Navigate} from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import App from "../layout/App";
import ProductDetails from "../../features/catalog/ProductDetails";
import AboutPage from "../../features/about/AboutPage";
import ContactPage from "../../features/contact/ContactPage";
import Catalog from "../../features/catalog/Catalog";
import ServerError from "../errors/ServerError";
import NotFoundError from "../errors/NotFoundError";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children:[
            {path: '', element: <HomePage />},
            {path: 'catalog', element: <Catalog />},
            {path: 'catalog/:id', element: <ProductDetails />},
            {path: 'about', element: <AboutPage />},
            {path: 'contact', element: <ContactPage />},
            {path: 'server-error', element: <ServerError />},
            {path: 'not-found', element: <NotFoundError />},
            {path: '*', element: <Navigate replace to='/not-found' />}
        ]
    }
])