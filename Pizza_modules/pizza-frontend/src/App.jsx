import { useState, useEffect } from 'react'
import './App.css'
import Pizza from './Pizza'
import pizzas from './assets/pizzas'

function App()
{
  const data = pizzas.map(pizza => <Pizza key={pizza.id} pizza={pizza} />)

  useEffect(() =>
  {
    async function getPizzas()
    {
      const res = await fetch('/api/pizzas')
      const pizzas = await res.json()
      console.log(pizzas)
    }
    getPizzas()
  })

  return (
    <>
      {data}
    </>
  )
}

export default App
