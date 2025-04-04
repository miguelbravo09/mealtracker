import React, { useEffect, useState } from 'react';
import axios from 'axios';

const MealList = () => {
    const [meals, setMeals] = useState([]);

    useEffect(() => {
        // Fetch meals from the API
        axios.get('https://api.miguelbravo.org/api/meals')
            .then(response => {
                setMeals(response.data);
            })
            .catch(error => {
                console.error('Error fetching meals:', error);
            });
    }, []);

    return (
        <div>
            <h2>Meals</h2>
            {meals.length === 0 ? (
                <p>No meals found.</p>
            ) : (
                <ul>
                    {meals.map(meal => (
                        <li key={meal.id}>
                            <h3>{meal.name}</h3>
                            <p>{meal.description}</p>
                            <p>Category: {meal.category?.name}</p>
                            <ul>
                                {meal.ingredients?.map(ingredient => (
                                    <li key={ingredient.id}>{ingredient.name}</li>
                                ))}
                            </ul>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default MealList;