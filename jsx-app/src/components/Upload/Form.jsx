import { useState } from "react"


export const UploadForm = () => {

    const [username, setUsername] = useState('')
    const [key, setKey] = useState('')
    const [file, setFile] = useState()


    const handleSubmit = (e) => {
        e.preventDefault()
        if (username.length < 4){
            alert('the username must be at least 4 characters')
            return
        } else if (key.length < 12){
            alert('the key must be at least 12 characters')
            return
        }
        console.log("username : " + username)
        console.log("key : " + key)
        console.log("file : " + file.name)
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


    return (
        <>
            <h3>Uploading Form</h3>
            <form form='uploadForm' onSubmit={handleSubmit}>
                <h4>Username</h4>
                <input type='text' placeholder="username" onChange={handleUsernameChange}/>
                <h4>File</h4>
                {/*
                    TODO -- encrypt the file
                */}
                <input type='file' onChange={handleFileChange}/> 
                <h4>Secret Key</h4>
                <input type='text' placeholder="Secret Key" onChange={handleKeyChange}/>
                <button>Send</button>
            </form>
        </>
    )
}