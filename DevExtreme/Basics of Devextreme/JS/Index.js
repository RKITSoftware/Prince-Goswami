$(function () {
    // create and configure component
    $('#buttonContainer').dxButton({
        text: 'Click me!',
        onClick: function () {
            console.log('Created a button  and configured it to alert message');
        },
    });

    // Get and Set properties
    var buttonInstance = $('#buttonContainer').dxButton('option');
    $('#buttonContainer2').dxButton(buttonInstance);
    $('#buttonContainer2').dxButton({
        onClick: () => {
            console.log(
                'Instance copied from #buttonContainer and set it into new button',
            );
        },
    });

    // implement method calling and removing component
    $('#buttonContainer3').dxButton({
        text : "Remove above buttons",
        onClick : () => {
            confirm("This Button will remove above buttons");
            $('#buttonContainer').remove();
            $('#buttonContainer2').remove();
        }
    });
    

});
