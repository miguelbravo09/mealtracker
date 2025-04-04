import React, { useState } from 'react';
import axios from 'axios';

const MealForm = ({ meal, onSave }) => {
    const [name, setName] = useState(meal?.name || '');
    const [description, setDescription] = useState(meal?.description || '');
    const [categoryId, setCategoryId] = useState(meal?.categoryId || '');

    const handleSubmit = async (e) => {
        e.preventDefault();
        const newMeal = { name, description, categoryId };

        try {
            if (meal) {
                // Update existing meal
                await axios.put(`https://api.miguelbravo.org/api/meals${meal.id}`, newMeal);
            } else {
                // Add new meal
                await axios.post('https://api.miguelbravo.org/api/meals', newMeal);
            }
            onSave();
        } catch (error) {
            console.error('Error saving meal:', error);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Name:</label>
                <input
                    type="text"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    required
                />
            </div>
            <div>
                <label>Description:</label>
                <textarea
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />
            </div>
            <div>
                <label>Category ID:</label>
                <input
                    type="number"
                    value={categoryId}
                    onChange={(e) => setCategoryId(e.target.value)}
                    required
                />
            </div>
            <button type="submit">{meal ? 'Update' : 'Add'} Meal</button>
        </form>
    );
};

export default MealForm;