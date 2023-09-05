
//Populating city based on selected state value

document.getElementById('state').addEventListener('change', populateCities);
function populateCities() {
    var stateInput = document.getElementById('state');
    var cityInput = document.getElementById('city');
    cityInput.innerHTML = '<option>--select--</option>';
    var selectedState = stateInput.value.toLowerCase();

    if (selectedState === 'kerala') {
        populateCityOptions(['Kochi', 'Thiruvananthapuram', 'Kozhikode', 'Thrissur'], cityInput);
    } else if (selectedState === 'tamil nadu') {
        populateCityOptions(['Chennai', 'Coimbatore', 'Madurai', 'Tiruchirappalli'], cityInput);
    } else if (selectedState === 'haryana') {
        populateCityOptions(['Gurgaon', 'Faridabad', 'Hisar', 'Panipat'], cityInput);
    } else if (selectedState === 'goa') {
        populateCityOptions(['Panaji', 'Margao', 'Vasco da Gama', 'Mapusa'], cityInput);
    }
}

function populateCityOptions(cities, cityInput) {
    cities.forEach(function (city) {
        var option = document.createElement('option');
        option.text = city;
        option.value = city;
        cityInput.add(option);
    });
}