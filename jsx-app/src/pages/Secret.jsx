import axios from "axios";
import { useState } from "react"

export const Secret = () =>{

    const [secret, Setsecret] = useState('');
    const [size, Setsize] = useState('');
    const [maxUpload, setMaxUpload] = useState('');

    const handleSecretChange = (e) => {
        Setsecret(e.target.value)
    }

    const handleSizeChange = (e) => {
        Setsize(e.target.value)
    }

    const handleMaxUploadChange = (e) => {
        setMaxUpload(e.target.value)
    }

    const handleSubmit = async (e) => {
        e.preventDefault();

        const data = {
            secret : secret,
            maxSize : size,
            maxUpload : maxUpload
        }

        await axios.post('http://localhost:5080/secret', data)
        .catch(error =>{
            console.error("Error occured during the POST /secret request :", error);
        });
    }

    return (
        <>
            <form onSubmit={handleSubmit}>
                <h4>Add your Secret in the database</h4>
                <input type="text" onChange={handleSecretChange}/>
                <h4>Max Size</h4>
                <input type="number" onChange={handleSizeChange}/>
                <h4>Max uploads</h4>
                <input type="number" onChange={handleMaxUploadChange}/>
                <button>Send secret</button>
            </form>
        </>
    )
}