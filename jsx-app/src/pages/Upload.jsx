import { Link } from "react-router-dom"
import { UploadForm } from "../components/Upload/Form"
import { Navigation } from "../components/Nav"

export const Upload = () => {
    return (
        <>
            <h2>Here you will upload your files</h2>
            <UploadForm/>
            <Navigation/>
        </>
    )
}