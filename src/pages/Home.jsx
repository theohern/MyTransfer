import { Link } from "react-router-dom"

export const Home = () => {
    return <>
        <h1>Home page</h1>
        <nav>
          <Link to='/upload'>Upload</Link>
          <Link to='/download'>Download</Link>
        </nav>
    </>
}

