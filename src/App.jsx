import { useState } from 'react'
import { createBrowserRouter, RouterProvider, Link } from 'react-router-dom'
import { Home } from './pages/Home'

const router = createBrowserRouter([
  {
    path : '/',
    element : <Home/>
  },
  {
    path : '/upload',
    element : <div>Upload</div>
  },
  {
    path : '/download',
    element : <div>Download</div>
  }
])



function App() {



  return <RouterProvider router={router}/>
}


export default App
