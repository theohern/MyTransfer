import { useState } from "react"
import { Navigation } from "../components/Nav"

export const Formulaire = () => {

    const [firstname, setFirstname] = useState('john doe')
    const [lastname, setLastname] = useState('mister blue')

    const handleChange = (e) =>{
        setFirstname(e.target.value)
    }

    const reset = () => {
        setFirstname('')
    }


    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(new FormData(e.target))
    }


    return (
        <>
            <form>  {/* c'est un champ controlé */}
                <input type='text' name='firstname' value={firstname} onChange={handleChange}/>
                {firstname}
                <button onClick={reset} type='button'>Reset</button>
            </form>

            <form onSubmit={handleSubmit}> {/*c'est un champ on-controlé, on s'occupe du changement une fois que tout est fini (que le user send) */}
                <input type='text' name='lastname'/>
                {lastname}
                <button>Send</button>
            </form>
            <Navigation/>
        </>
    )
}