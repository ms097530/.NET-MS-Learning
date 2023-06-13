import React, { useState } from 'react'

export default function Pizza({ pizza })
{
    const [data, setData] = useState(pizza)
    const [dirty, setDirty] = useState(false)

    const update = (value, fieldName, obj) =>
    {
        setData({ ...obj, [fieldName]: value })
        setDirty(true)
    }

    const onSave = () =>
    {
        setDirty(false)
        // make rest call
    }

    return (
        <div>
            <h3>
                <input onChange={(e) => update(e.target.value, 'name', data)} value={data.name} />
            </h3>
            <div>
                <input onChange={(e) => update(e.target.value, 'description', data)} value={data.description} />
            </div>
            {
                dirty ? <div><button onClick={onSave}>Save</button></div>
                    : null
            }
        </div>
    )
}
