import { useState } from 'react'
import viteLogo from '/vite.svg'


const title = "Bonjour tout le monde !"
const message = "c'est mon premier truc en react vraiment.."
const showTitle = false
const todos = [
  'faire plusieurs pages',
  'créer un backend',
  'ajouter une database'
]

function App() {

  const handleClick = () =>{
    alert ("J'ai appuyé sur un bouton")
  }

  return (
    <>
      { showTitle ? <h1 onClick={handleClick}>{title}</h1> : <h1>Not the real title</h1>}
      <p>{message}</p>
      <ul>
        {todos.map(todo => (<li key={todo}>{todo}</li>))}
      </ul>
      <Bye color='red' hidden>tout le monde</Bye>
    </>
  )
}

function Bye ({color, children, hidden}) {

  const props = {
    id : 'boss',
    className : 'myclass'
  }

  if (hidden){
    return null
  }
  return (
    <h3 style={{color : color}} {...props}>Au revoir {children} </h3>
  )
}

export default App
