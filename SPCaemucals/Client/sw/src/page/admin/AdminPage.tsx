import {Paper} from "@mui/material";
import React, {useEffect} from "react";
import {useDispatch} from "react-redux";
import {setTitle} from "../../features/Nav/NavSlice";
import pageData from "../../type/PageData.json"

export function AdminPage() {
    const dispatch = useDispatch();

    useEffect(()=>{
        dispatch(setTitle(pageData.admin.pageTitle))
        document.title = pageData.admin.pageTitle;
    },[dispatch])
    return <Paper sx={{margin: "10px"}}>
    </Paper>;
}