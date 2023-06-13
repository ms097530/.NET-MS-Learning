import React, { useState } from 'react'

let pizzas = [{
    id: 1, name: 'Cheese pizza', description: 'very cheesy'
},
{
    id: 2, name: 'Al Tono pizza', description: 'lots of tuna'
}]

export default function Pizza({ pizza })
{
    const [data, setData] = useState(pizza)
    const [dirty, setDirty] = useState(false)

    const update = (value, fieldName, obj) =>
    {
        setData({ ...obj, [fieldName]: value })
        setDirty(true)
    }

    return (
        <div>
            <h3>
                <input onChange={(e) => update(e.target.value, 'name', data)} value={data.name} />
            </h3>
        </div>
    )
}
