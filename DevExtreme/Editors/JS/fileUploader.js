$(() => {
    const fileUploader = $('#file-uploader')
        .dxFileUploader({
            multiple: false,
            labelText : "",
            accept: '*',
            value: [],
            uploadMode: 'instantely',
            uploadUrl:
                'https://js.devexpress.com/Demos/WidgetsGalleryDataService/api/ChunkUpload',
            allowCancelling: true,
            chunkSize: 400000,
            maxFileSize: 40000,
            onUploadStarted,
            onProgress: onUploadProgress,
            onValueChanged(e) {
                const files = e.value;
                if (files.length > 0) {
                    $('#selected-files .selected-item').remove();
                    $.each(files, (i, file) => {
                        const $selectedItem =
                            $('<div />').addClass('selected-item');
                        $selectedItem.append(
                            $('<span />').html(`Name: ${file.name}<br/>`),
                            $('<span />').html(`Size ${file.size} bytes<br/>`),
                            $('<span />').html(`Type ${file.type}<br/>`),
                            $('<span />').html(
                                `Last Modified Date: ${file.lastModifiedDate}`,
                            ),
                        );
                        $selectedItem.appendTo($('#selected-files'));
                    });
                    $('#selected-files').show();
                } else {
                    $('#selected-files').hide();
                }
            },
        })
        .dxFileUploader('instance');

    const dropzone = $('#file-dropzone').dxFileUploader({
          dialogTrigger: '#dropzone-external',
          dropZone: '#dropzone-external',
          multiple: false,
          allowedFileExtensions: ['.jpg', '.jpeg', '.gif', '.png'],
          uploadMode: 'instantly',
          uploadUrl: 'https://js.devexpress.com/Demos/NetCore/FileUploader/Upload',
          visible: false,
          onDropZoneEnter(e) {
            if (e.dropZoneElement.id === 'dropzone-external') { toggleDropZoneActive(e.dropZoneElement, true); }
          },
          onDropZoneLeave(e) {
            if (e.dropZoneElement.id === 'dropzone-external') { toggleDropZoneActive(e.dropZoneElement, false); }
          },
          onUploaded(e) {
            const { file } = e;
            const dropZoneText = document.getElementById('dropzone-text');
            const fileReader = new FileReader();
            fileReader.onload = function () {
              toggleDropZoneActive(document.getElementById('dropzone-external'), false);
              const dropZoneImage = document.getElementById('dropzone-image');
              dropZoneImage.src = fileReader.result;
            };
            fileReader.readAsDataURL(file);
            dropZoneText.style.display = 'none';
            uploadProgressBar.option({
              visible: false,
              value: 0,
            });
          },
          onProgress(e) {
            uploadProgressBar.option('value', (e.bytesLoaded / e.bytesTotal) * 100);
          },
          onUploadStarted() {
            toggleImageVisible(false);
            uploadProgressBar.option('visible', true);
          },
        }).dxFileUploader('instance');

        
    $('#accept-option').dxSelectBox({
        inputAttr: { 'aria-label': 'Accept Option' },
        dataSource: [
            { name: 'All types', value: '*' },
            { name: 'Images', value: 'image/*' },
            { name: 'Videos', value: 'video/*' },
        ],
        valueExpr: 'value',
        displayExpr: 'name',
        value: '*',
        onValueChanged(e) {
            fileUploader.option('accept', e.value);
        },
    });

    $('#upload-option').dxSelectBox({
        items: ['instantly', 'useButtons'],
        value: 'instantly',
        inputAttr: { 'aria-label': 'Upload Option' },
        onValueChanged(e) {
            fileUploader.option('uploadMode', e.value);
        },
    });

    $('#multiple-option').dxCheckBox({
        value: false,
        text: 'Allow multiple files selection',
        onValueChanged(e) {
            fileUploader.option('multiple', e.value);
        },
    });

    $('#max-file-size').dxTextBox({
        //items: ['instantly', 'useButtons'],
        //value: 'instantly',
        placeHolder: 'Max File Size',
        inputAttr: { 'aria-label': 'Max File Size in MB' },
        onValueChanged(e) {
            fileUploader.option('maxFileSize', e.value * 1000000);
            fileUploader.option(
                'invalidMaxFileSizeMessage',
                'File size must be less then ' + e.value * 1000000,
            );
            console.log(fileUploader.option('maxFileSize'));
        },
    });

    $('#min-file-size').dxTextBox({
        //items: ['instantly', 'useButtons'],
        //value: 'instantly',
        placeHolder: 'Min File Size',
        inputAttr: { 'aria-label': 'Min File Size in MB' },
        onValueChanged(e) {
            fileUploader.option('minFileSize', e.value * 1000000);
            fileUploader.option(
                'invalidMinFileSizeMessage',
                'File size must be greater then ' + e.value * 1000000,
            );
            console.log(fileUploader.option('minFileSize'));
        },
    });

    function onUploadStarted() {
        $('#chunk-panel').innerHTML = '';
    }
    function onUploadProgress(e) {
        $('#chunk-panel').appendChild(
            addChunkInfo(e.segmentSize, e.bytesLoaded, e.bytesTotal),
        );
    }

    function addChunkInfo(segmentSize, loaded, total) {
        const result = document.createElement('DIV');
        console.log(result);
        result.appendChild(createSpan('Chunk size:'));
        result.appendChild(
            createSpan(getValueInKb(segmentSize), 'segment-size'),
        );
        result.appendChild(createSpan(', Uploaded:'));
        result.appendChild(createSpan(getValueInKb(loaded), 'loaded-size'));
        result.appendChild(createSpan('/'));
        result.appendChild(createSpan(getValueInKb(total), 'total-size'));

        return result;
    }
    function getValueInKb(value) {
        return `${(value / 1024).toFixed(0)}kb`;
    }
    function createSpan(text, className) {
        const result = document.createElement('SPAN');
        if (className) {
            result.className = `${className} dx-theme-accent-as-text-color`;
        }
        result.innerText = text;
        return result;
    }
});
