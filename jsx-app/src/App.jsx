import { useState } from 'react'
import { createBrowserRouter, RouterProvider, Link } from 'react-router-dom'
import { Home } from './pages/Home'
import { Upload } from './pages/Upload'
import { Download } from './pages/Download'
import { Formulaire } from './pages/Form'
import { Secret } from './pages/Secret'

const router = createBrowserRouter([
  {
    path : '/',
    element : <Home/>
  },
  {
    path : '/upload',
    element : <Upload/>
  },
  {
    path : '/download',
    element : <Download/>
  },
  {
    path : '/learning/form',
    element : <Formulaire/>
  },
  {
    path : "/secret",
    element : <Secret/>
  }
])



function App() {



  return <RouterProvider router={router}/>
}


export default App
