$(() => {
    // Function to initialize buttons
    const initButton = (id, text, page) => {
        $(`#${id}`).dxButton({
            text: text,
            onClick: () => {
                console.log(`redirecting to ${text} page...!!!`);
                window.location.href = `${page}.html`;
            },
        });
    };

    // Initialize buttons in ascending order
    initButton('buttons', 'Buttons', 'Button');
    initButton('checkBox', 'CheckBox', 'CheckBox');
    initButton('dateBox', 'Date Box', 'DateBox');
    initButton('dropDown', 'Drop Down', 'DropDown');
    initButton('fileUploader', 'File Uploader', 'FileUploader');
    initButton('numberBox', 'Number Box', 'NumberBox');
    initButton('radioGroup', 'Radio Group', 'RadioGroup');
    initButton('selectBox', 'Select Box', 'SelectBox');
    initButton('textArea', 'Text Area', 'TextArea');
    initButton('textBox', 'Text Box', 'TextBox');
    initButton('validation', 'Validation', 'Validation');
});
