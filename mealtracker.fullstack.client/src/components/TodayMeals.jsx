import React, { useEffect, useState } from 'react';
import axios from 'axios';

const TodayMeals = () => {
    const [breakfast, setBreakfast] = useState([]);
    const [lunch, setLunch] = useState([]);
    const [dinner, setDinner] = useState([]);

    useEffect(() => {
        // Fetch meals from the API
        axios.get('https://api.miguelbravo.org/api/meals/1')
            .then(response => {
                setBreakfast(response.data);
            })
            .catch(error => {
                console.error('Error fetching meal:', error);
            });
    }, []);

    useEffect(() => {
        // Fetch meals from the API
        axios.get('https://api.miguelbravo.org/api/meals/2')
            .then(response => {
                setLunch(response.data);
            })
            .catch(error => {
                console.error('Error fetching meal:', error);
            });
    }, []);

    useEffect(() => {
        // Fetch meals from the API
        axios.get('https://api.miguelbravo.org/api/meals/4')
            .then(response => {
                setDinner(response.data);
            })
            .catch(error => {
                console.error('Error fetching meal:', error);
            });
    }, []);

    return (
        <div>
            <h2>Breakfast</h2>
            <h3>{breakfast.name}</h3>
            <p>{breakfast.description}</p>
            <p>Category: {breakfast.category?.name}</p>
            <ul>
                {breakfast.ingredients?.map(ingredient => (
                    <li key={ingredient.id}>{ingredient.name}</li>
                ))}
            </ul>
            <h2>Lunch</h2>
            <h3>{lunch.name}</h3>
            <p>{lunch.description}</p>
            <p>Category: {lunch.category?.name}</p>
            <ul>
                {lunch.ingredients?.map(ingredient => (
                    <li key={ingredient.id}>{ingredient.name}</li>
                ))}
            </ul>
            <h2>Dinner</h2>
            <h3>{dinner.name}</h3>
            <p>{dinner.description}</p>
            <p>Category: {dinner.category?.name}</p>
            <ul>
                {dinner.ingredients?.map(ingredient => (
                    <li key={ingredient.id}>{ingredient.name}</li>
                ))}
            </ul>
        </div>
    );
};

export default TodayMeals;