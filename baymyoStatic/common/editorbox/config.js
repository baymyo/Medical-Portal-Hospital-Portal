﻿/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'tr';
    // config.uiColor = '#AADC6E';
    // Toolbar configuration generated automatically by the editor based on config.toolbarGroups.

    config.toolbar = [
	{ name: 'document', groups: ['mode', 'document', 'doctools'] },
    { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] }, { items: ['Source', '-', 'Templates'] },
	{ name: 'clipboard', groups: ['clipboard', 'undo'], items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
	{ name: 'editing', groups: ['find', 'selection', 'spellchecker'], items: ['Find', 'Replace', '-', 'SelectAll', '-', 'Scayt'] },
    //{ name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'] },
	'/',
	{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
	{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
	'/',
	{ name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
	{ name: 'colors', items: ['TextColor', 'BGColor'] },
	{ name: 'tools', items: ['Maximize', 'ShowBlocks'] },
    //{ name: 'others', items: ['-'] },
	{name: 'about', items: ['About']}];

    // Toolbar groups configuration.
    config.toolbarGroups = [
	{ name: 'document', groups: ['mode', 'document', 'doctools'] },
	{ name: 'clipboard', groups: ['clipboard', 'undo'] },
	{ name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
	{ name: 'forms' },
	'/',
	{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
	{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'] },
	{ name: 'links' },
	{ name: 'insert' },
	'/',
	{ name: 'styles' },
	{ name: 'colors' },
	{ name: 'tools' },
	{ name: 'others' },
	{ name: 'about'}];

    config.filebrowserBrowseUrl = "/common/fileman/index.html";
    config.filebrowserImageBrowseUrl = "/common/fileman/index.html?type=image";
    config.removeDialogTabs = "link:upload;image:upload";

//    config.entities = false;
//    config.basicEntities = false;
//    config.entities_greek = false;
//    config.entities_latin = false;
    config.language = 'tr';
};
