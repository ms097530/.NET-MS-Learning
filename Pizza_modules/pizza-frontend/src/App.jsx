import { useState } from 'react'
import './App.css'
import Pizza from './Pizza'
import pizzas from './assets/pizzas'

function App()
{
  const data = pizzas.map(pizza => <Pizza key={pizza.id} pizza={pizza} />)

  return (
    <>
      {data}
    </>
  )
}

export default App
