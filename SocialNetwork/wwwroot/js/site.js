const phoneNumberInput = document.getElementById('phoneNumber');

phoneNumberInput.addEventListener('input', function () {
    const input = phoneNumberInput.value.replace(/\D/g, ''); // Elimina caracteres no numéricos

    if (input.length > 10) {
        input = input.substr(0, 10); // Limita a 10 dígitos
    }

    // Formatea el número según el formato deseado
    let formattedInput = '';
    if (input.length > 3) {
        formattedInput += `(${input.substr(0, 3)})`;
    }
    if (input.length > 6) {
        formattedInput += `-${input.substr(3, 3)}`;
    }
    if (input.length > 7) {
        formattedInput += `-${input.substr(6)}`;
    }

    phoneNumberInput.value = formattedInput;
});






