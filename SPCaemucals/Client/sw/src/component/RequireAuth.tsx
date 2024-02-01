import React, { ReactElement, useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { Navigate, useLocation } from 'react-router-dom';
import { selectUser } from '../app/store';
import { reLogin } from "../features/auth/authSlice";
import { useAppDispatch } from "../app/hooks";

interface RequireAuthProps {
    children: ReactElement;
}

const RequireAuth: React.FC<RequireAuthProps> = ({ children }) => {
    const user = useSelector(selectUser);
    const dispatch = useAppDispatch();
    const location = useLocation();

    // State to manage whether the re-login attempt has been completed
    const [authAttempted, setAuthAttempted] = useState(false);

    useEffect(() => {
        const checkAuth = async () => {
            if (!user) {
                const resultAction = await dispatch(reLogin());
                if (!reLogin.fulfilled.match(resultAction)) {
                    // If reLogin is not fulfilled, set authAttempted to true without changing user state
                    setAuthAttempted(true);
                }
            } else {
                // If user exists, consider authentication already checked
                setAuthAttempted(true);
            }
        };

        checkAuth();
    }, [user, dispatch]);

    // Render logic based on authentication state and auth attempt state
    if (!user && authAttempted) {
        // Redirect to login if user is not authenticated after reLogin attempt
        return <Navigate to="/login" state={{ from: location }} replace />;
    } else if (!authAttempted) {
        // Optionally, render a loading indicator while authentication is being checked
        return <div>Loading...</div>;
    }

    // Render children if user is authenticated
    return children;
};

export default RequireAuth;
