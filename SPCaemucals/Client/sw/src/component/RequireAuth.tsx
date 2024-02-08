import React, { ReactElement, useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { Navigate, useLocation } from 'react-router-dom';
import { selectUser } from '../app/store';
import { reLogin } from "../features/auth/authSlice";
import { useAppDispatch } from "../app/hooks";
import Cookies from "js-cookie";

interface RequireAuthProps {
    children: ReactElement;
}

const RequireAuth: React.FC<RequireAuthProps> = ({ children }) => {
    const user = useSelector(selectUser);
    const dispatch = useAppDispatch();
    const location = useLocation();

    const [attemp, setAttemp] = useState<boolean>(false)

    // State to manage whether the re-login attempt has been completed



    useEffect(() => {

        const checkAuth = async () => {
            if (!user) {

                const token = Cookies.get('token');

                if (token) {

                    const resultAction = await dispatch(reLogin());
                    if (!reLogin.fulfilled.match(resultAction)) {
                        // If reLogin is not fulfilled, set authAttempted to true without changing user state
                        setAttemp(true);
                    }
                }else{
                    setAttemp(true);
                }
            }
        };

        checkAuth();
    }, [user,dispatch]);

    if (!user && !attemp){
        return <><div>Loading...</div></>
    }else {
        if (user) {

            return children;
            // Redirect to login if user is not authenticated after reLogin attempt
        }else {
            return <Navigate to="/login" state={{ from: location }} replace />;
        }
    }
    // Render logic based on authentication state and auth attempt state


    // Render children if user is authenticated
    return children;
};

export default RequireAuth;
