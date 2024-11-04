import { useState } from 'react'
import { createBrowserRouter, RouterProvider, Link } from 'react-router-dom'
import { Home } from './pages/Home'
import { Upload } from './pages/Upload'
import { Download } from './pages/Download'
import { Formulaire } from './pages/Form'

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
  }
])



function App() {



  return <RouterProvider router={router}/>
}


export default App
