

$('#summernote').summernote({
    placeholder: 'Hello stand alone ui',
    tabsize: 2,
    height: 120,
    toolbar: [
        ['style', ['style']],
        ['font', ['bold', 'underline', 'clear']],
        ['color', ['color']],
        ['para', ['ul', 'ol', 'paragraph']],
        ['table', ['table']],
        ['insert', ['link', 'picture', 'video']],
        ['view', ['fullscreen', 'codeview', 'help']]
    ]
});

function minifyHTML(html) {
    return content = html.replace(/\n/g, '')
        .replace(/\r/g, '')
        .replace(/\s{2,}/g, ' ')
        .replace(/>\s+</g, '><');
}