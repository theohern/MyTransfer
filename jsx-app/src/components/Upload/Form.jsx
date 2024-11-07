import { useState } from "react";
import axios from 'axios';


export const UploadForm = () => {

    const [username, setUsername] = useState('')
    const [key, setKey] = useState('')
    const [file, setFile] = useState()
    const [secret, setSecret] = useState('')
    const [maxDownloads, setMaxDownloads] = useState('')


    const handleSubmit = async (e) => {
        e.preventDefault()
        if (username.length < 4){
            alert('the username must be at least 4 characters')
            return
        } else if (key.length < 12){
            alert('the key must be at least 12 characters')
            return
        }

        const data = {
            username : username,
            size : file.size, 
            date : file.date, 
            type : file.type, 
            fileName : file.name,
            secret : secret,
            file : -1,
            publickey : -1,
            maxDownloads : maxDownloads
        }

        await axios.post('http://localhost:5080/upload', data)
        .catch(error =>{
            console.error("Error occured during the POST /upload request :", error);
        });
            
    }

    const handleUsernameChange = (e) => {
        setUsername(e.target.value)
    }

    const handleKeyChange = (e) => {
        setKey(e.target.value)
    }

    const handleFileChange = (e) => {
        setFile(e.target.files[0])
    }

    const handleSecretChange = (e) => {
        setSecret(e.target.value)
    }

    const handleMaxDownloads = (e) => {
        setMaxDownloads(e.target.value)
    }


    return (
        <>
            <h3>Uploading Form</h3>
            <form form='uploadForm' onSubmit={handleSubmit}>
                <h4>Username</h4>
                <input type='text' placeholder="username" onChange={handleUsernameChange}/>
                <h4>File</h4>
                <input type='file' onChange={handleFileChange}/>
                <h4>Number of times you can Download the file</h4>
                <input type='number' onChange={handleMaxDownloads}/> 
                <h4>Password</h4>
                <input type='text' placeholder="Password" onChange={handleKeyChange}/>
                <h4>Secret Key</h4>
                <input type='text' placeholder="Secret Key" onChange={handleSecretChange}/>
                <button>Send</button>
            </form>
        </>
    )
}