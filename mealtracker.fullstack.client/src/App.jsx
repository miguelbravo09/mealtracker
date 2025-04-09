import React, { useState } from 'react';
import MealList from './components/MealList';
import MealForm from './components/MealForm';
import TodayMeals from './components/TodayMeals';
import './App.css';

const App = () => {
    const [refresh, setRefresh] = useState(false);

    const handleSave = () => {
        setRefresh(!refresh); // Refresh the meal list after saving
    };

    return (
        <div className="App">
            <h1>Meal Tracker</h1>
            <p className="app-description">
                Track your daily meals and maintain healthy eating habits!
                {refresh && <span className="update-message">List updated!</span>}
            </p>
            <TodayMeals />
            <MealForm onSave={handleSave} />
            <MealList key={refresh} />
        </div>
    );
};

export default App;