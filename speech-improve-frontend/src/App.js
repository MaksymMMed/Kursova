import logo from './logo.svg';
import './App.css';
import LoginForm from './Pages/LoginPage/LoginForm';
import ReactDOM from 'react-dom/client';
import './index.css';
import reportWebVitals from './reportWebVitals';
import {createBrowserRouter, RouterProvider,} from "react-router-dom";
import RegisterPage from './Pages/RegisterPage/RegisterPage';
import MainPage from './Pages/MainPage/MainPage';
import ProfilePage from './Pages/ProfilePage/ProfilePage';

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <LoginForm/>,
    },
    {
      path: "/SignUp",
      element: <RegisterPage/>,
    },
    {
      path: "/MainPage",
      element: <MainPage/>,
    },
    {
      path: "/ProfilePage",
      element: <ProfilePage/>,
    },
    ]);
  return (
    <RouterProvider router={router} />
  );
}

export default App;
