$(() => {
    // Define items for radio groups
    const genders = ['Male', 'Female', 'Other'];
    const marriageStatuses = ['Single', 'Married', 'Divorced'];
    const priorityEntities = [
        { id: 1, text: 'Low' },
        { id: 2, text: 'Medium' },
        { id: 3, text: 'High' }
    ];

    // Initialize the gender radio group
    const genderRadioGroup = $('#gender').dxRadioGroup({
        items: genders,
        value: genders[0], // Default value
        layout: 'horizontal',
        itemTemplate(itemData, _, itemElement) {
            itemElement.text(itemData).addClass('gender-radio-item');
        },
        focusStateEnabled: true, // Enable focus state
        showClearButton: true, // Show clear button
    }).dxRadioGroup('instance');

    // Initialize the marriage status radio group
    const marriageStatusRadioGroup = $('#marriageStatus').dxRadioGroup({
        items: marriageStatuses,
        value: marriageStatuses[0], // Default value
        layout: 'vertical',
        itemTemplate(itemData, _, itemElement) {
            itemElement.text(itemData).addClass('marriage-status-item');
        },
        focusStateEnabled: true, // Enable focus state
        showClearButton: true, // Show clear button
    }).dxRadioGroup('instance');

    // Initialize the priority radio group
    const priorityRadioGroup = $('#priority').dxRadioGroup({
        items: priorityEntities,
        valueExpr: 'id',
        displayExpr: 'text',
        value: priorityEntities[1].id, // Default value
        layout: 'horizontal',
        itemTemplate(itemData, _, itemElement) {
            itemElement.text(itemData.text).addClass('priority-item');
        },
        focusStateEnabled: true, // Enable focus state
        showClearButton: true, // Show clear button
    }).dxRadioGroup('instance');

    // Handle the Reset Button functionality with dxButton
    $('#resetButton').dxButton({
        text: 'Reset Selections',
        onClick() {
            genderRadioGroup.reset(); // Reset gender selection
            marriageStatusRadioGroup.reset(); // Reset marriage status selection
            priorityRadioGroup.reset(); // Reset priority selection
            console.log('Selections have been reset.');
        }
    });

    // Handle the Submit Button functionality with dxButton
    $('#submitButton').dxButton({
        text: 'Submit Form',
        onClick() {
            // Get selected values from the radio groups
            const selectedGender = genderRadioGroup.option('value');
            const selectedMarriageStatus = marriageStatusRadioGroup.option('value');
            const selectedPriority = priorityRadioGroup.option('value');

            // Display the selected values
            console.log('Selected Gender:', selectedGender);
            console.log('Selected Marriage Status:', selectedMarriageStatus);
            console.log('Selected Priority:', selectedPriority);

            // Optional: Perform form submission or other actions
            alert('Form Submitted!\n' +
                'Gender: ' + selectedGender + '\n' +
                'Marriage Status: ' + selectedMarriageStatus + '\n' +
                'Priority: ' + selectedPriority);
        }
    });

    // Optional: Add validation or further functionality for the submit button
    function validateSelections() {
        const genderValue = genderRadioGroup.option('value');
        const marriageStatusValue = marriageStatusRadioGroup.option('value');
        const priorityValue = priorityRadioGroup.option('value');

        if (!genderValue || !marriageStatusValue || !priorityValue) {
            alert('Please make sure all selections are made before submitting.');
        } else {
            $('#submitButton').dxButton('instance').click(); // Trigger submit if valid
        }
    }

    // Example of validation on submit click
    $('#submitButton').on('click', function () {
        validateSelections();
    });
});
