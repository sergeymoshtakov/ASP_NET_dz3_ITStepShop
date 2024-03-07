$(document).ready(function () {
    CKEDITOR.replace('Description', {
        filebrowserUploadUrl: '/UploadCKEDITOR/Upload',
        filebrowserBrowseUrl: '/UploadCKEDITOR/FileBrowser'
    })
})
