window.fbAsyncInit = (function() {
	Fb.init({ 
	appId: '509295025790446', 
	status: true, 
	cookie: true,
	xfbml: true
	});
});

(function(){
    var e = document.createElement('script');
    e.type = 'text/javascript';
    e.src = document.location.protocol + "//connect.facebook.net/en_US/all.js";
    e.async = true;
    document.getElementById('fb-root').appendChild(e)
}(document, 'script', 'facebook-jssdk'))


$(document).ready(function(){
    $('#share_button').click(function(e){
        e.preventDefault();
        FB.ui(
        {
            method: 'feed',
            name: 'This is the content of the "name" field.',
            link: ' http://www.hyperarts.com/',
            picture: 'http://www.hyperarts.com/external-xfbml/share-image.gif',
            caption: 'This is the content of the "caption" field.',
            description: 'This is the content of the "description" field, below the caption.',
            message: ''
        });
    });
});
