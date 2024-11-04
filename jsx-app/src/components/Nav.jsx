import { Link } from "react-router-dom"

export const Navigation = () =>{
    return (
        <nav>
            <Link to='/'>Home</Link>
            <Link to='/upload'>Upload</Link>
            <Link to='/download'>Download</Link>
            <Link to='/learning/form'>Learning Form</Link>
        </nav>
    )
}