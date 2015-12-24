var posX;
var canvasIntro;
var context;
var address;
var IsDoorOpen = false;
$(function () {
    var tamañoAreaDibujo = $('#divIntro').width();
    canvasIntro1 = document.getElementById('canvas-intro2');
    context1 = canvasIntro1.getContext('2d');
    canvasIntro2 = document.getElementById('canvas-intro');
    $('#canvas-intro').width(tamañoAreaDibujo / 4 + 'px');
    $('#canvas-intro2').width(tamañoAreaDibujo / 4 + 'px');
    context2 = canvasIntro2.getContext('2d');
    posX1 = 0;
    posX2 = 250;
    address1 = 0;
    address2 = 1;
    drawDoor1();
    drawDoor2();
    openDoor();
    //animateTopBar();
    animateVideo($('#divVideoIntro1'), $('#frameVideo1'));
    animateVideo($('#divVideoIntro2'), $('#frameVideo2'));
    animateVideo($('#divVideoIntro3'), $('#frameVideo3'));
    parallaxEfect();
    startTime();
    makeDate();
    animateTitle();
    getLocation();
});

function drawDoor1() {
    if (address1 == 0) {
        posX1++;
    }
    else {
        posX1--;
    }
    if (posX1 == 50)
        address1 = 1;
    if (posX1 == 0)
        address1 = 0;

    canvasIntro1.width = canvasIntro1.width; // limpia el canvas
    context1.beginPath();
    context1.fillStyle = "cornflowerblue";
    context1.strokeStyle = "cornflowerblue";
    context1.fillRect(posX1, 0, 50, 150);
    context1.fillStyle = "black";
    context1.strokeStyle = "black";
    context1.fillRect(posX1, 55, 10, 50);
    context1.closePath();
}

function drawDoor2() {
    if (address2 == 1) {
        posX2--;
    }
    else {
        posX2++;
    }
    if (posX2 == 200)
        address2 = 0;
    if (posX2 == 250)
        address2 = 1;
    canvasIntro2.width = canvasIntro2.width; // limpia el canvas
    context2.beginPath();
    context2.fillStyle = "cornflowerblue";
    context2.strokeStyle = "cornflowerblue";
    context2.fillRect(posX2, 0, 50, 150);
    context2.fillStyle = "black";
    context2.strokeStyle = "black";
    context2.fillRect(posX2 + 40, 55, 10, 50);
    context2.closePath();
}

function openDoor() {
    $('#divIntro').mouseover(function () {
        if (!IsDoorOpen) {
            IsDoorOpen = true;
            setInterval("drawDoor1()", 70);
            setInterval("drawDoor2()", 70);
            $('#divIntro').animate({
                backgroundColor: "#6495ed",
                color: "cornflowerblue"
            }, 1000);
        }
    });
    $('#divIntro').click(function () {
        var action = '/Search/Index';
        window.location.href = action;
    });
}

function animateVideo(divVideo, frameVideo) {
    var widthVideo = $('#divBody').width() / 1.5;
    var heightVideo = $('#divBody').height() / 1.5;
    divVideo.mouseover(function () {
        $('#divVideoPlay').empty();
        var autoPlay = frameVideo.attr('src') + '?autoplay=1';
        frameVideo.clone().attr('src', autoPlay).attr('width', '100%').attr('height', '100%').appendTo('#divVideoPlay');
        $('#divVideoPlay').dialog({
            width: widthVideo,
            height: heightVideo,
            show: "fade",
            hide: "fade",
            resizable: "false",
            modal: "true",
            close: function (event, ui) { $('#divVideoPlay').empty(); }
        });
    });
}

function parallaxEfect() {
    $(window).scroll(function () {
        var ancho = $(window).width();

        // Si trabajamos con una imagen desactivamos el background-size:cover;
        if (ancho <= 1350) {
            $('body').css({
                'background-size': 'initial'
            });
        }
        var barra = $(window).scrollTop();
        var posicion = (barra * 0.10);

        $('body').css({
            'background-position': '0 -' + posicion + 'px'
        });
    });
}

function startTime() {
    today = new Date();
    h = today.getHours();
    m = today.getMinutes();
    s = today.getSeconds();
    m = checkTime(m);
    s = checkTime(s);
    document.getElementById('reloj').innerHTML = h + ":" + m + ":" + s;
    t = setTimeout('startTime()', 500);
}

function checkTime(i) {
    if (i < 10) { i = "0" + i; }
    return i;
}

function makeArray() {
    for (i = 0; i < makeArray.arguments.length; i++)
        this[i + 1] = makeArray.arguments[i];
}

function makeDate() {
    var months = new makeArray('Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo',
'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre');
    var date = new Date();
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var yy = date.getYear();
    var year = (yy < 1000) ? yy + 1900 : yy;
    $('#divDate').html("Hoy es " + day + " de " + months[month] + " del " + year);
}

function animateTitle() {
    $("#divTitle").show("slide", {}, 1500);
}

function getLocation() {
    if (navigator.geolocation)
    {
        navigator.geolocation.getCurrentPosition(showPosition);        
    }
    else
    {
        //alert("Geolocation is not supported by this browser.");
    }
}

function showPosition(position) {
    var latT = position.coords.latitude;
    var lngT = position.coords.longitude;
    console.log(latT);
    console.log(lngT);
    var geocoder = new google.maps.Geocoder();
    var latlng = { lat: parseFloat(latT), lng: parseFloat(lngT) };
    console.log(latlng);
    //geocoder.geocode({ 'location': latlng, 'componentRestrictions': { 'country': 'CO' } }, function (results, status) {
    geocoder.geocode({ 'location': latlng}, function (results, status) {
        var country;
        if (status === google.maps.GeocoderStatus.OK) {            
            for (var i = 0; i < results.length; i++) {
                if (results[i].types[0] == 'country') {
                    country = results[i];
                    break;
                }
            }
            if (country) {
                $.ajax({
                    url: 'http://localhost:59927/Account/UpdateUserCountry',
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({
                        country: country.formatted_address
                    }),
                    async: true,
                    processData: false,
                    cache: false,
                    success: function (data) {
                        if (data.Country == 'COLOMBIA') {
                            if($('#imgCountry').attr('src') == '')
                            {
                                $('#divImgCountry').show();
                                $('#imgCountry').attr('src', 'http://localhost:59927/Content/images/co.png')
                            }
                        }
                        else {
                            if (data.Country == 'ECUADOR') {
                                if ($('#imgCountry').attr('src') == '') {
                                    $('#divImgCountry').show();
                                    $('#imgCountry').attr('src', 'http://localhost:59927/Content/images/ec.png')
                                }
                            }
                            else {
                                $('#divImgCountry').hide();
                            }
                        }
                    },
                    error: function (xhr, errorText) {
                    }
                });
                //window.alert(country.formatted_address);
            } else {
                //window.alert('No results found');
            }
        } else {
            //window.alert('Geocoder failed due to: ' + status);
        }
    });
}

////<![CDATA[
//    // Nieve en el blog
//    var snowStorm=function(e,d){function g(a,d){isNaN(d)&&(d=0);return Math.random()*a+d}function o(){e.setTimeout(function(){a.start(true)},20);a.events.remove(i?d:e,"mousemove",o)}function r(){if(!a.excludeMobile||!s)a.freezeOnBlur?a.events.add(i?d:e,"mousemove",o):o();a.events.remove(e,"load",r)}this.flakesMax=128;this.flakesMaxActive=64;this.animationInterval=40;this.excludeMobile=true;this.flakeBottom=null;this.followMouse=true;this.snowColor="#ffffff";this.snowCharacter="&bull;";this.snowStick=true;
//    this.targetElement=null;this.useMeltEffect=true;this.usePositionFixed=this.useTwinkleEffect=false;this.freezeOnBlur=true;this.flakeRightOffset=this.flakeLeftOffset=0;this.flakeHeight=this.flakeWidth=8;this.vMaxX=5;this.vMaxY=4;this.zIndex=0;var a=this,y=this,i=navigator.userAgent.match(/msie/i),z=navigator.userAgent.match(/msie 6/i),A=navigator.appVersion.match(/windows 98/i),s=navigator.userAgent.match(/mobile/i),B=i&&d.compatMode==="BackCompat",t=s||B||z,h=null,k=null,j=null,m=null,u=null,v=null,
//    p=1,n=false,q;a:{try{d.createElement("div").style.opacity="0.5"}catch(C){q=false;break a}q=true}var w=false,x=d.createDocumentFragment();this.timers=[];this.flakes=[];this.active=this.disabled=false;this.meltFrameCount=20;this.meltFrames=[];this.events=function(){function a(b){var b=f.call(b),c=b.length;l?(b[1]="on"+b[1],c>3&&b.pop()):c===3&&b.push(false);return b}function d(a,c){var e=a.shift(),f=[b[c]];if(l)e[f](a[0],a[1]);else e[f].apply(e,a)}var l=!e.addEventListener&&e.attachEvent,f=Array.prototype.slice,
//    b={add:l?"attachEvent":"addEventListener",remove:l?"detachEvent":"removeEventListener"};return{add:function(){d(a(arguments),"add")},remove:function(){d(a(arguments),"remove")}}}();this.randomizeWind=function(){var c;c=g(a.vMaxX,0.2);u=parseInt(g(2),10)===1?c*-1:c;v=g(a.vMaxY,0.2);if(this.flakes)for(c=0;c<this.flakes.length;c++)this.flakes[c].active&&this.flakes[c].setVelocities()};this.scrollHandler=function(){var c;m=a.flakeBottom?0:parseInt(e.scrollY||d.documentElement.scrollTop||d.body.scrollTop,
//    10);isNaN(m)&&(m=0);if(!n&&!a.flakeBottom&&a.flakes)for(c=a.flakes.length;c--;)a.flakes[c].active===0&&a.flakes[c].stick()};this.resizeHandler=function(){e.innerWidth||e.innerHeight?(h=e.innerWidth-(!i?16:16)-a.flakeRightOffset,j=a.flakeBottom?a.flakeBottom:e.innerHeight):(h=(d.documentElement.clientWidth||d.body.clientWidth||d.body.scrollWidth)-(!i?8:0)-a.flakeRightOffset,j=a.flakeBottom?a.flakeBottom:d.documentElement.clientHeight||d.body.clientHeight||d.body.scrollHeight);k=parseInt(h/2,10)};this.resizeHandlerAlt=
//    function(){h=a.targetElement.offsetLeft+a.targetElement.offsetWidth-a.flakeRightOffset;j=a.flakeBottom?a.flakeBottom:a.targetElement.offsetTop+a.targetElement.offsetHeight;k=parseInt(h/2,10)};this.freeze=function(){var c;if(a.disabled)return false;else a.disabled=1;for(c=a.timers.length;c--;)clearInterval(a.timers[c])};this.resume=function(){if(a.disabled)a.disabled=0;else return false;a.timerInit()};this.toggleSnow=function(){a.flakes.length?(a.active=!a.active,a.active?(a.show(),a.resume()):(a.stop(),
//    a.freeze())):a.start()};this.stop=function(){var c;this.freeze();for(c=this.flakes.length;c--;)this.flakes[c].o.style.display="none";a.events.remove(e,"scroll",a.scrollHandler);a.events.remove(e,"resize",a.resizeHandler);a.freezeOnBlur&&(i?(a.events.remove(d,"focusout",a.freeze),a.events.remove(d,"focusin",a.resume)):(a.events.remove(e,"blur",a.freeze),a.events.remove(e,"focus",a.resume)))};this.show=function(){var a;for(a=this.flakes.length;a--;)this.flakes[a].o.style.display="block"};this.SnowFlake=
//    function(a,e,l,f){var b=this;this.type=e;this.x=l||parseInt(g(h-20),10);this.y=!isNaN(f)?f:-g(j)-12;this.vY=this.vX=null;this.vAmpTypes=[1,1.2,1.4,1.6,1.8];this.vAmp=this.vAmpTypes[this.type];this.melting=false;this.meltFrameCount=a.meltFrameCount;this.meltFrames=a.meltFrames;this.twinkleFrame=this.meltFrame=0;this.active=1;this.fontSize=10+this.type/5*10;this.o=d.createElement("div");this.o.innerHTML=a.snowCharacter;this.o.style.color=a.snowColor;this.o.style.position=n?"fixed":"absolute";this.o.style.width=
//    a.flakeWidth+"px";this.o.style.height=a.flakeHeight+"px";this.o.style.fontFamily="arial,verdana";this.o.style.overflow="hidden";this.o.style.fontWeight="normal";this.o.style.zIndex=a.zIndex;x.appendChild(this.o);this.refresh=function(){if(isNaN(b.x)||isNaN(b.y))return false;b.o.style.left=b.x+"px";b.o.style.top=b.y+"px"};this.stick=function(){t||a.targetElement!==d.documentElement&&a.targetElement!==d.body?b.o.style.top=j+m-a.flakeHeight+"px":a.flakeBottom?b.o.style.top=a.flakeBottom+"px":(b.o.style.display=
//    "none",b.o.style.top="auto",b.o.style.bottom="0px",b.o.style.position="fixed",b.o.style.display="block")};this.vCheck=function(){if(b.vX>=0&&b.vX<0.2)b.vX=0.2;else if(b.vX<0&&b.vX>-0.2)b.vX=-0.2;if(b.vY>=0&&b.vY<0.2)b.vY=0.2};this.move=function(){var d=b.vX*p;b.x+=d;b.y+=b.vY*b.vAmp;if(b.x>=h||h-b.x<a.flakeWidth)b.x=0;else if(d<0&&b.x-a.flakeLeftOffset<-a.flakeWidth)b.x=h-a.flakeWidth-1;b.refresh();if(j+m-b.y<a.flakeHeight)b.active=0,a.snowStick?b.stick():b.recycle();else{if(a.useMeltEffect&&b.active&&
//    b.type<3&&!b.melting&&Math.random()>0.998)b.melting=true,b.melt();if(a.useTwinkleEffect)if(b.twinkleFrame)b.twinkleFrame--,b.o.style.visibility=b.twinkleFrame&&b.twinkleFrame%2===0?"hidden":"visible";else if(Math.random()>0.9)b.twinkleFrame=parseInt(Math.random()*20,10)}};this.animate=function(){b.move()};this.setVelocities=function(){b.vX=u+g(a.vMaxX*0.12,0.1);b.vY=v+g(a.vMaxY*0.12,0.1)};this.setOpacity=function(a,b){if(!q)return false;a.style.opacity=b};this.melt=function(){!a.useMeltEffect||!b.melting?
//    b.recycle():b.meltFrame<b.meltFrameCount?(b.meltFrame++,b.setOpacity(b.o,b.meltFrames[b.meltFrame]),b.o.style.fontSize=b.fontSize-b.fontSize*(b.meltFrame/b.meltFrameCount)+"px",b.o.style.lineHeight=a.flakeHeight+2+a.flakeHeight*0.75*(b.meltFrame/b.meltFrameCount)+"px"):b.recycle()};this.recycle=function(){b.o.style.display="none";b.o.style.position=n?"fixed":"absolute";b.o.style.bottom="auto";b.setVelocities();b.vCheck();b.meltFrame=0;b.melting=false;b.setOpacity(b.o,1);b.o.style.padding="0px";b.o.style.margin=
//    "0px";b.o.style.fontSize=b.fontSize+"px";b.o.style.lineHeight=a.flakeHeight+2+"px";b.o.style.textAlign="center";b.o.style.verticalAlign="baseline";b.x=parseInt(g(h-a.flakeWidth-20),10);b.y=parseInt(g(j)*-1,10)-a.flakeHeight;b.refresh();b.o.style.display="block";b.active=1};this.recycle();this.refresh()};this.snow=function(){for(var c=0,d=0,e=0,f=null,f=a.flakes.length;f--;)a.flakes[f].active===1?(a.flakes[f].move(),c++):a.flakes[f].active===0?d++:e++,a.flakes[f].melting&&a.flakes[f].melt();if(c<a.flakesMaxActive&&
//    (f=a.flakes[parseInt(g(a.flakes.length),10)],f.active===0))f.melting=true};this.mouseMove=function(c){if(!a.followMouse)return true;c=parseInt(c.clientX,10);c<k?p=-2+c/k*2:(c-=k,p=c/k*2)};this.createSnow=function(c,d){var e;for(e=0;e<c;e++)if(a.flakes[a.flakes.length]=new a.SnowFlake(a,parseInt(g(6),10)),d||e>a.flakesMaxActive)a.flakes[a.flakes.length-1].active=-1;y.targetElement.appendChild(x)};this.timerInit=function(){a.timers=!A?[setInterval(a.snow,a.animationInterval)]:[setInterval(a.snow,a.animationInterval*
//    3),setInterval(a.snow,a.animationInterval)]};this.init=function(){var c;for(c=0;c<a.meltFrameCount;c++)a.meltFrames.push(1-c/a.meltFrameCount);a.randomizeWind();a.createSnow(a.flakesMax);a.events.add(e,"resize",a.resizeHandler);a.events.add(e,"scroll",a.scrollHandler);a.freezeOnBlur&&(i?(a.events.add(d,"focusout",a.freeze),a.events.add(d,"focusin",a.resume)):(a.events.add(e,"blur",a.freeze),a.events.add(e,"focus",a.resume)));a.resizeHandler();a.scrollHandler();a.followMouse&&a.events.add(i?d:e,"mousemove",
//    a.mouseMove);a.animationInterval=Math.max(20,a.animationInterval);a.timerInit()};this.start=function(c){if(w){if(c)return true}else w=true;if(typeof a.targetElement==="string"&&(c=a.targetElement,a.targetElement=d.getElementById(c),!a.targetElement))throw Error('Snowstorm: Unable to get targetElement "'+c+'"');if(!a.targetElement)a.targetElement=!i?d.documentElement?d.documentElement:d.body:d.body;if(a.targetElement!==d.documentElement&&a.targetElement!==d.body)a.resizeHandler=a.resizeHandlerAlt;
//        a.resizeHandler();a.usePositionFixed=a.usePositionFixed&&!t;n=a.usePositionFixed;if(h&&j&&!a.disabled)a.init(),a.active=true};a.events.add(e,"load",r,false);return this}(window,document);
// //]]>