$(document).ready(function () {
    var urlParams = new URLSearchParams(window.location.search);
    var ckEditorFuncNum = urlParams.get("CKEditorFuncNum");
    $(function () {
        $('img').click(function () {
            var fileurl = '/uplods/' + $(this).attr('title');
            alert(ckEditorFuncNum);
            window.opener.CKEDITOR.tools.callFunction(ckEditorFuncNum, fileurl);
            window.close();
        }).hover(function () {
            $(this).css('cursor','pointer')
        })
    })
})