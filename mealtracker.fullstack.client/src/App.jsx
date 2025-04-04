import React, { useState } from 'react';
import MealList from './components/MealList';
import MealForm from './components/MealForm';
import './App.css';

const App = () => {
    const [refresh, setRefresh] = useState(false);

    const handleSave = () => {
        setRefresh(!refresh); // Refresh the meal list after saving
    };

    return (
        <div className="App">
            <h1>Meal Tracker</h1>
            <MealForm onSave={handleSave} />
            <MealList key={refresh} />
        </div>
    );
};

export default App;