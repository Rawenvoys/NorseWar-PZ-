$(function () {
    var clicked;
    var clickedBox;
    var backpackPropertiesArr = new Array();

    setBackpackProperties();

    function setBackpackProperties() {
        backpackPropertiesArr = new Array();
        for (var i = 0; i < $(".box").length; i++) {
            if ($(".box:eq(" + i + ")").children().length === 0) {
                backpackPropertiesArr.push({ empty: true, x: $(".box:eq(" + i + ")").position().left, y: $(".box:eq(" + i + ")").position().top });
            } else {
                backpackPropertiesArr.push({ empty: false, x: $(".box:eq(" + i + ")").position().left, y: $(".box:eq(" + i + ")").position().top });
            }
        }
    }

    var dragged = null;
    $('.draggable').draggable({
        drag: function () {
            dragged = $(".draggable").index(this);
            clicked = dragged;
        }
    })
	.mousedown(function () {
	    //tu pobieranie id przeciąganego elementu
	    dragged = $(".draggable").index(this);
	    clicked = dragged;
	    $(".draggable:eq(" + clicked + ")").css({ "zIndex": "60" });
	    clickedBox = $(this).parent().index(".box");
	})
	.mouseup(function () {
	    if (dragged !== null) {
	        if ($(".draggable:eq(" + dragged + ")").hasClass("helmet")) {

	            var cx = $("#helmetcontainer").position().left + 2;
	            var cy = $("#helmetcontainer").position().top + 2;
	            var hw = $("#helmetcontainer").width();
	            var hh = $("#helmetcontainer").height();
	            if ($(".draggable:eq(" + clicked + ")").position().left >= (cx - 10) && $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)
						&& $('#helmetcontainer').is(':empty')) {

	                //wsadzenie itemka na puste miejsce
	                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                $(".draggable:eq(" + clicked + ")").css({ left: cx, top: cy });
	                $(".draggable:eq(" + clicked + ")").appendTo("#helmetcontainer");

	                makeSpaceInBackpack();
	                //alert(backpackPropertiesArr[0].empty);
	            } else if (!$('#helmetcontainer').is(':empty') && $(".draggable:eq(" + clicked + ")").position().left >= (cx - 10)
						&& $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)) {

	                //zamiana itemka

	                var cpI = $('.draggable:eq(' + clicked + ')').html();
	                var copiedItemFromBP = $('.draggable:eq(' + clicked + ')').clone();
	                var copiedEquiped = $('#helmetcontainer').children().clone();
	                //warunek na nie usuwanie itemka, gdy się na niego kliknie
	                if ($("#helmetcontainer").children().html() !== copiedItemFromBP.html()) {
	                    //zamiana itemek

	                    //boost statów -> sprawdzenie czy jest założona itemka, bo sprawdzanie wyżej nie działa XD
	                    var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                    var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                    var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                    var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                    var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                    var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                    if ($("#helmetcontainer").children().attr("itemname") !== undefined) {
	                        // założony itemek
	                        $("#strvalue").html(parseInt($("#strvalue").html()) - parseInt($("#helmetcontainer").children().attr("strbonus")) + draggedStrBonus);
	                        $("#agivalue").html(parseInt($("#agivalue").html()) - parseInt($("#helmetcontainer").children().attr("agibonus")) + draggedAgiBonus);
	                        $("#vitvalue").html(parseInt($("#vitvalue").html()) - parseInt($("#helmetcontainer").children().attr("vitbonus")) + draggedVitBonus);
	                        $("#dexvalue").html(parseInt($("#dexvalue").html()) - parseInt($("#helmetcontainer").children().attr("dexbonus")) + draggedDexBonus);
	                        $("#intvalue").html(parseInt($("#intvalue").html()) - parseInt($("#helmetcontainer").children().attr("intbonus")) + draggedIntBonus);
	                        $("#lukvalue").html(parseInt($("#lukvalue").html()) - parseInt($("#helmetcontainer").children().attr("lukbonus")) + draggedLukBonus);

	                        //tu jeszcze jakiś ajax do bazy

	                    } else {
	                        // włożenie itemka w puste miejsce
	                        $("#strvalue").html(parseInt($("#strvalue").html()) + draggedStrBonus);
	                        $("#agivalue").html(parseInt($("#agivalue").html()) + draggedAgiBonus);
	                        $("#vitvalue").html(parseInt($("#vitvalue").html()) + draggedVitBonus);
	                        $("#dexvalue").html(parseInt($("#dexvalue").html()) + draggedDexBonus);
	                        $("#intvalue").html(parseInt($("#intvalue").html()) + draggedIntBonus);
	                        $("#lukvalue").html(parseInt($("#lukvalue").html()) + draggedLukBonus);
	                        // i tu ajax do bazy
	                        $.ajax({
	                            type: 'POST',
	                            url: $("#emptySpace").data('request-url'),
	                            data: 'id=' + parseInt($('.draggable:eq('+clicked+')').attr('iditem')),
	                            success: function (data) {
                                    //lul
	                            },
	                            error: function (data) {
                                    //lul2
	                            }
	                        });

	                    }

	                    backpackPropertiesArr[clickedBox].empty = true;
	                    $("#helmetcontainer").children().appendTo("#littlehelper");
	                    $(".box:eq(" + clickedBox + ")").children().appendTo("#helmetcontainer");
	                    $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                    $(".box:eq(" + clickedBox + ")").children().css({ left: 0, top: 0 });
	                    $("#helmetcontainer").children().css({ left: 0, top: 0 });
	                    backpackPropertiesArr[clickedBox].empty = false;
	                    $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                    $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                }
	            } else {
	                //może tu zrobię wsadzanie do plecaka xD
	                var posX = $(".draggable:eq(" + dragged + ")").position().left;
	                var posY = $(".draggable:eq(" + dragged + ")").position().top;
	                for (var i = 0; i < backpackPropertiesArr.length; i++) {

	                    //jeśli trzymany itemek znajduje się nad którymś divem z klasą box...
	                    if (posX >= backpackPropertiesArr[i].x &&
                                posY >= backpackPropertiesArr[i].y &&
                                posX <= (backpackPropertiesArr[i].x + hw) &&
                                posY <= (backpackPropertiesArr[i].y + hh)) {
	                        //... jeśli pozycja któregoś diva z klasą box spełnia warunek i jeśli dany div jest pusty...
	                        if (parseInt(backpackPropertiesArr[i].x) === parseInt($(".box:eq(" + i + ")").position().left) &&
                                    parseInt(backpackPropertiesArr[i].y) === parseInt($(".box:eq(" + i + ")").position().top) &&
                                    backpackPropertiesArr[i].empty) {

	                            //... jeśli rodzic przeciąganego itemka ma klasę box...
	                            if ($(".draggable:eq(" + dragged + ")").parent().hasClass("box")) {
	                                //wstaw w itemek w puste miejsce
	                                backpackPropertiesArr[clickedBox].empty = true;
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //odejmij staty z boosta
	                                var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                                var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                                var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                                var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                                var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                                var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                                $("#strvalue").html(parseInt($("#strvalue").html()) - draggedStrBonus);
	                                $("#agivalue").html(parseInt($("#agivalue").html()) - draggedAgiBonus);
	                                $("#vitvalue").html(parseInt($("#vitvalue").html()) - draggedVitBonus);
	                                $("#dexvalue").html(parseInt($("#dexvalue").html()) - draggedDexBonus);
	                                $("#intvalue").html(parseInt($("#intvalue").html()) - draggedIntBonus);
	                                $("#lukvalue").html(parseInt($("#lukvalue").html()) - draggedLukBonus);
	                                //tu ajax do bazy

	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                    }
	                }
	            }
	        } else if ($(".draggable:eq(" + dragged + ")").hasClass("weapon")) {

	            var cx = $("#weaponcontainer").position().left + 2;
	            var cy = $("#weaponcontainer").position().top + 2;
	            var hw = $("#weaponcontainer").width();
	            var hh = $("#weaponcontainer").height();
	            if ($(".draggable:eq(" + clicked + ")").position().left >= (cx - 10) && $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)
						&& $('#weaponcontainer').is(':empty')) {

	                //wsadzenie itemka na puste miejsce
	                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                $(".draggable:eq(" + clicked + ")").css({ left: cx, top: cy });
	                $(".draggable:eq(" + clicked + ")").appendTo("#weaponcontainer");

	                makeSpaceInBackpack();
	                //alert(backpackPropertiesArr[0].empty);
	            } else if (!$('#weaponcontainer').is(':empty') && $(".draggable:eq(" + clicked + ")").position().left >= (cx - 10)
						&& $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)) {

	                //zamiana itemka

	                var cpI = $('.draggable:eq(' + clicked + ')').html();
	                var copiedItemFromBP = $('.draggable:eq(' + clicked + ')').clone();
	                var copiedEquiped = $('#weaponcontainer').children().clone();
	                //warunek na nie usuwanie itemka, gdy się na niego kliknie
	                if ($("#weaponcontainer").children().html() !== copiedItemFromBP.html()) {
	                    //zamiana itemek

	                    //boost statów -> sprawdzenie czy jest założona itemka, bo sprawdzanie wyżej nie działa XD
	                    var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                    var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                    var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                    var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                    var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                    var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                    if ($("#weaponcontainer").children().attr("itemname") !== undefined) {
	                        // założony itemek
	                        $("#strvalue").html(parseInt($("#strvalue").html()) - parseInt($("#weaponcontainer").children().attr("strbonus")) + draggedStrBonus);
	                        $("#agivalue").html(parseInt($("#agivalue").html()) - parseInt($("#weaponcontainer").children().attr("agibonus")) + draggedAgiBonus);
	                        $("#vitvalue").html(parseInt($("#vitvalue").html()) - parseInt($("#weaponcontainer").children().attr("vitbonus")) + draggedVitBonus);
	                        $("#dexvalue").html(parseInt($("#dexvalue").html()) - parseInt($("#weaponcontainer").children().attr("dexbonus")) + draggedDexBonus);
	                        $("#intvalue").html(parseInt($("#intvalue").html()) - parseInt($("#weaponcontainer").children().attr("intbonus")) + draggedIntBonus);
	                        $("#lukvalue").html(parseInt($("#lukvalue").html()) - parseInt($("#weaponcontainer").children().attr("lukbonus")) + draggedLukBonus);

	                        //tu jeszcze jakiś ajax do bazy

	                    } else {
	                        // włożenie itemka w puste miejsce
	                        $("#strvalue").html(parseInt($("#strvalue").html()) + draggedStrBonus);
	                        $("#agivalue").html(parseInt($("#agivalue").html()) + draggedAgiBonus);
	                        $("#vitvalue").html(parseInt($("#vitvalue").html()) + draggedVitBonus);
	                        $("#dexvalue").html(parseInt($("#dexvalue").html()) + draggedDexBonus);
	                        $("#intvalue").html(parseInt($("#intvalue").html()) + draggedIntBonus);
	                        $("#lukvalue").html(parseInt($("#lukvalue").html()) + draggedLukBonus);
	                        // i tu ajax do bazy
	                        $.ajax({
	                            type: 'POST',
	                            url: $("#emptySpace").data('request-url'),
	                            data: 'id=' + parseInt($('.draggable:eq(' + clicked + ')').attr('iditem')),
	                            success: function (data) {
	                                //lul
	                            },
	                            error: function (data) {
	                                //lul2
	                            }
	                        });
	                    }

	                    backpackPropertiesArr[clickedBox].empty = true;
	                    $("#weaponcontainer").children().appendTo("#littlehelper");
	                    $(".box:eq(" + clickedBox + ")").children().appendTo("#weaponcontainer");
	                    $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                    $(".box:eq(" + clickedBox + ")").children().css({ left: 0, top: 0 });
	                    $("#weaponcontainer").children().css({ left: 0, top: 0 });
	                    backpackPropertiesArr[clickedBox].empty = false;
	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                }
	            } else {
	                //może tu zrobię wsadzanie do plecaka xD
	                var posX = $(".draggable:eq(" + dragged + ")").position().left;
	                var posY = $(".draggable:eq(" + dragged + ")").position().top;
	                for (var i = 0; i < backpackPropertiesArr.length; i++) {

	                    //jeśli trzymany itemek znajduje się nad którymś divem z klasą box...
	                    if (posX >= backpackPropertiesArr[i].x &&
                                posY >= backpackPropertiesArr[i].y &&
                                posX <= (backpackPropertiesArr[i].x + hw) &&
                                posY <= (backpackPropertiesArr[i].y + hh)) {
	                        //... jeśli pozycja któregoś diva z klasą box spełnia warunek i jeśli dany div jest pusty...
	                        if (parseInt(backpackPropertiesArr[i].x) === parseInt($(".box:eq(" + i + ")").position().left) &&
                                    parseInt(backpackPropertiesArr[i].y) === parseInt($(".box:eq(" + i + ")").position().top) &&
                                    backpackPropertiesArr[i].empty) {

	                            //... jeśli rodzic przeciąganego itemka ma klasę box...
	                            if ($(".draggable:eq(" + dragged + ")").parent().hasClass("box")) {
	                                //wstaw w itemek w puste miejsce
	                                backpackPropertiesArr[clickedBox].empty = true;
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //odejmij staty z boosta
	                                var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                                var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                                var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                                var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                                var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                                var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                                $("#strvalue").html(parseInt($("#strvalue").html()) - draggedStrBonus);
	                                $("#agivalue").html(parseInt($("#agivalue").html()) - draggedAgiBonus);
	                                $("#vitvalue").html(parseInt($("#vitvalue").html()) - draggedVitBonus);
	                                $("#dexvalue").html(parseInt($("#dexvalue").html()) - draggedDexBonus);
	                                $("#intvalue").html(parseInt($("#intvalue").html()) - draggedIntBonus);
	                                $("#lukvalue").html(parseInt($("#lukvalue").html()) - draggedLukBonus);
	                                //tu ajax do bazy


	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                    }
	                }
	            }
	        } else if ($(".draggable:eq(" + dragged + ")").hasClass("boots")) {

	            var cx = $("#bootscontainer").position().left + 2;
	            var cy = $("#bootscontainer").position().top + 2;
	            var hw = $("#bootscontainer").width();
	            var hh = $("#bootscontainer").height();
	            if ($(".draggable:eq(" + clicked + ")").position().left >= (cx - 10) && $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)
						&& $('#bootscontainer').is(':empty')) {

	                //wsadzenie itemka na puste miejsce
	                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                $(".draggable:eq(" + clicked + ")").css({ left: cx, top: cy });
	                $(".draggable:eq(" + clicked + ")").appendTo("#bootscontainer");

	                makeSpaceInBackpack();
	                //alert(backpackPropertiesArr[0].empty);
	            } else if (!$('#bootscontainer').is(':empty') && $(".draggable:eq(" + clicked + ")").position().left >= (cx - 10)
						&& $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)) {

	                //zamiana itemka

	                var cpI = $('.draggable:eq(' + clicked + ')').html();
	                var copiedItemFromBP = $('.draggable:eq(' + clicked + ')').clone();
	                var copiedEquiped = $('#bootscontainer').children().clone();
	                //warunek na nie usuwanie itemka, gdy się na niego kliknie
	                if ($("#bootscontainer").children().html() !== copiedItemFromBP.html()) {
	                    //zamiana itemek
	                    if (clickedBox !== -1) {
	                        //boost statów -> sprawdzenie czy jest założona itemka, bo sprawdzanie wyżej nie działa XD
	                        var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                        var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                        var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                        var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                        var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                        var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                        if ($("#bootscontainer").children().attr("itemname") !== undefined) {
	                            // założony itemek
	                            $("#strvalue").html(parseInt($("#strvalue").html()) - parseInt($("#bootscontainer").children().attr("strbonus")) + draggedStrBonus);
	                            $("#agivalue").html(parseInt($("#agivalue").html()) - parseInt($("#bootscontainer").children().attr("agibonus")) + draggedAgiBonus);
	                            $("#vitvalue").html(parseInt($("#vitvalue").html()) - parseInt($("#bootscontainer").children().attr("vitbonus")) + draggedVitBonus);
	                            $("#dexvalue").html(parseInt($("#dexvalue").html()) - parseInt($("#bootscontainer").children().attr("dexbonus")) + draggedDexBonus);
	                            $("#intvalue").html(parseInt($("#intvalue").html()) - parseInt($("#bootscontainer").children().attr("intbonus")) + draggedIntBonus);
	                            $("#lukvalue").html(parseInt($("#lukvalue").html()) - parseInt($("#bootscontainer").children().attr("lukbonus")) + draggedLukBonus);

	                            //tu jeszcze jakiś ajax do bazy

	                        } else {
	                            // włożenie itemka w puste miejsce
	                            $("#strvalue").html(parseInt($("#strvalue").html()) + draggedStrBonus);
	                            $("#agivalue").html(parseInt($("#agivalue").html()) + draggedAgiBonus);
	                            $("#vitvalue").html(parseInt($("#vitvalue").html()) + draggedVitBonus);
	                            $("#dexvalue").html(parseInt($("#dexvalue").html()) + draggedDexBonus);
	                            $("#intvalue").html(parseInt($("#intvalue").html()) + draggedIntBonus);
	                            $("#lukvalue").html(parseInt($("#lukvalue").html()) + draggedLukBonus);
	                            // i tu ajax do bazy
	                            $.ajax({
	                                type: 'POST',
	                                url: $("#emptySpace").data('request-url'),
	                                data: 'id=' + parseInt($('.draggable:eq(' + clicked + ')').attr('iditem')),
	                                success: function (data) {
	                                    //lul
	                                },
	                                error: function (data) {
	                                    //lul2
	                                }
	                            });
	                        }

	                        backpackPropertiesArr[clickedBox].empty = true;
	                        $("#bootscontainer").children().appendTo("#littlehelper");
	                        $(".box:eq(" + clickedBox + ")").children().appendTo("#bootscontainer");
	                        $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                        $(".box:eq(" + clickedBox + ")").children().css({ left: 0, top: 0 });
	                        $("#bootscontainer").children().css({ left: 0, top: 0 });
	                        backpackPropertiesArr[clickedBox].empty = false;
	                    } else {
	                        $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                    }
	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                }
	            } else {
	                //może tu zrobię wsadzanie do plecaka xD
	                var posX = $(".draggable:eq(" + dragged + ")").position().left;
	                var posY = $(".draggable:eq(" + dragged + ")").position().top;
	                for (var i = 0; i < backpackPropertiesArr.length; i++) {

	                    //jeśli trzymany itemek znajduje się nad którymś divem z klasą box...
	                    if (posX >= backpackPropertiesArr[i].x &&
                                posY >= backpackPropertiesArr[i].y &&
                                posX <= (backpackPropertiesArr[i].x + hw) &&
                                posY <= (backpackPropertiesArr[i].y + hh)) {
	                        //... jeśli pozycja któregoś diva z klasą box spełnia warunek i jeśli dany div jest pusty...
	                        if (parseInt(backpackPropertiesArr[i].x) === parseInt($(".box:eq(" + i + ")").position().left) &&
                                    parseInt(backpackPropertiesArr[i].y) === parseInt($(".box:eq(" + i + ")").position().top) &&
                                    backpackPropertiesArr[i].empty) {

	                            //... jeśli rodzic przeciąganego itemka ma klasę box...
	                            if ($(".draggable:eq(" + dragged + ")").parent().hasClass("box")) {
	                                //wstaw w itemek w puste miejsce
	                                backpackPropertiesArr[clickedBox].empty = true;
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //odejmij staty z boosta
	                                var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                                var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                                var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                                var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                                var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                                var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                                $("#strvalue").html(parseInt($("#strvalue").html()) - draggedStrBonus);
	                                $("#agivalue").html(parseInt($("#agivalue").html()) - draggedAgiBonus);
	                                $("#vitvalue").html(parseInt($("#vitvalue").html()) - draggedVitBonus);
	                                $("#dexvalue").html(parseInt($("#dexvalue").html()) - draggedDexBonus);
	                                $("#intvalue").html(parseInt($("#intvalue").html()) - draggedIntBonus);
	                                $("#lukvalue").html(parseInt($("#lukvalue").html()) - draggedLukBonus);
	                                //tu ajax do bazy

	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                    }
	                }
	            }
	        } else if ($(".draggable:eq(" + dragged + ")").hasClass("armor")) {

	            var cx = $("#armorcontainer").position().left + 2;
	            var cy = $("#armorcontainer").position().top + 2;
	            var hw = $("#armorcontainer").width();
	            var hh = $("#armorcontainer").height();
	            if ($(".draggable:eq(" + clicked + ")").position().left >= (cx - 10) && $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)
						&& $('#armorcontainer').is(':empty')) {

	                //wsadzenie itemka na puste miejsce
	                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                $(".draggable:eq(" + clicked + ")").css({ left: cx, top: cy });
	                $(".draggable:eq(" + clicked + ")").appendTo("#armorcontainer");

	                makeSpaceInBackpack();
	                //alert(backpackPropertiesArr[0].empty);
	            } else if (!$('#armorcontainer').is(':empty') && $(".draggable:eq(" + clicked + ")").position().left >= (cx - 10)
						&& $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)) {

	                //zamiana itemka

	                var cpI = $('.draggable:eq(' + clicked + ')').html();
	                var copiedItemFromBP = $('.draggable:eq(' + clicked + ')').clone();
	                var copiedEquiped = $('#armorcontainer').children().clone();
	                //warunek na nie usuwanie itemka, gdy się na niego kliknie
	                if ($("#armorcontainer").children().html() !== copiedItemFromBP.html()) {
	                    //zamiana itemek
	                    if (clickedBox !== -1) {
	                        //boost statów -> sprawdzenie czy jest założona itemka, bo sprawdzanie wyżej nie działa XD
	                        var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                        var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                        var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                        var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                        var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                        var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                        if ($("#armorcontainer").children().attr("itemname") !== undefined) {
	                            // założony itemek
	                            $("#strvalue").html(parseInt($("#strvalue").html()) - parseInt($("#armorcontainer").children().attr("strbonus")) + draggedStrBonus);
	                            $("#agivalue").html(parseInt($("#agivalue").html()) - parseInt($("#armorcontainer").children().attr("agibonus")) + draggedAgiBonus);
	                            $("#vitvalue").html(parseInt($("#vitvalue").html()) - parseInt($("#armorcontainer").children().attr("vitbonus")) + draggedVitBonus);
	                            $("#dexvalue").html(parseInt($("#dexvalue").html()) - parseInt($("#armorcontainer").children().attr("dexbonus")) + draggedDexBonus);
	                            $("#intvalue").html(parseInt($("#intvalue").html()) - parseInt($("#armorcontainer").children().attr("intbonus")) + draggedIntBonus);
	                            $("#lukvalue").html(parseInt($("#lukvalue").html()) - parseInt($("#armorcontainer").children().attr("lukbonus")) + draggedLukBonus);

	                            //tu jeszcze jakiś ajax do bazy

	                        } else {
	                            // włożenie itemka w puste miejsce
	                            $("#strvalue").html(parseInt($("#strvalue").html()) + draggedStrBonus);
	                            $("#agivalue").html(parseInt($("#agivalue").html()) + draggedAgiBonus);
	                            $("#vitvalue").html(parseInt($("#vitvalue").html()) + draggedVitBonus);
	                            $("#dexvalue").html(parseInt($("#dexvalue").html()) + draggedDexBonus);
	                            $("#intvalue").html(parseInt($("#intvalue").html()) + draggedIntBonus);
	                            $("#lukvalue").html(parseInt($("#lukvalue").html()) + draggedLukBonus);
	                            // i tu ajax do bazy
	                            $.ajax({
	                                type: 'POST',
	                                url: $("#emptySpace").data('request-url'),
	                                data: 'id=' + parseInt($('.draggable:eq(' + clicked + ')').attr('iditem')),
	                                success: function (data) {
	                                    //lul
	                                },
	                                error: function (data) {
	                                    //lul2
	                                }
	                            });
	                        }

	                        backpackPropertiesArr[clickedBox].empty = true;
	                        $("#armorcontainer").children().appendTo("#littlehelper");
	                        $(".box:eq(" + clickedBox + ")").children().appendTo("#armorcontainer");
	                        $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                        $(".box:eq(" + clickedBox + ")").children().css({ left: 0, top: 0 });
	                        $("#armorcontainer").children().css({ left: 0, top: 0 });
	                        backpackPropertiesArr[clickedBox].empty = false;
	                    } else {
	                        $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                    }

	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                }
	            } else {
	                //może tu zrobię wsadzanie do plecaka xD
	                var posX = $(".draggable:eq(" + dragged + ")").position().left;
	                var posY = $(".draggable:eq(" + dragged + ")").position().top;
	                for (var i = 0; i < backpackPropertiesArr.length; i++) {
	                    //jeśli trzymany itemek znajduje się nad którymś divem z klasą box...
	                    if (posX >= backpackPropertiesArr[i].x &&
                                posY >= backpackPropertiesArr[i].y &&
                                posX <= (backpackPropertiesArr[i].x + hw) &&
                                posY <= (backpackPropertiesArr[i].y + hh)) {
	                        //... jeśli pozycja któregoś diva z klasą box spełnia warunek i jeśli dany div jest pusty...
	                        if (parseInt(backpackPropertiesArr[i].x) === parseInt($(".box:eq(" + i + ")").position().left) &&
                                    parseInt(backpackPropertiesArr[i].y) === parseInt($(".box:eq(" + i + ")").position().top) &&
                                    backpackPropertiesArr[i].empty) {

	                            //... jeśli rodzic przeciąganego itemka ma klasę box...
	                            if ($(".draggable:eq(" + dragged + ")").parent().hasClass("box")) {
	                                //wstaw w itemek w puste miejsce
	                                backpackPropertiesArr[clickedBox].empty = true;
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //odejmij staty z boosta
	                                var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                                var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                                var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                                var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                                var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                                var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                                $("#strvalue").html(parseInt($("#strvalue").html()) - draggedStrBonus);
	                                $("#agivalue").html(parseInt($("#agivalue").html()) - draggedAgiBonus);
	                                $("#vitvalue").html(parseInt($("#vitvalue").html()) - draggedVitBonus);
	                                $("#dexvalue").html(parseInt($("#dexvalue").html()) - draggedDexBonus);
	                                $("#intvalue").html(parseInt($("#intvalue").html()) - draggedIntBonus);
	                                $("#lukvalue").html(parseInt($("#lukvalue").html()) - draggedLukBonus);
	                                //tu ajax do bazy

	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                    }
	                }
	            }
	        } else if ($(".draggable:eq(" + dragged + ")").hasClass("shield")) {

	            var cx = $("#shieldcontainer").position().left + 2;
	            var cy = $("#shieldcontainer").position().top + 2;
	            var hw = $("#shieldcontainer").width();
	            var hh = $("#shieldcontainer").height();
	            if ($(".draggable:eq(" + clicked + ")").position().left >= (cx - 10) && $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)
						&& $('#shieldcontainer').is(':empty')) {

	                //wsadzenie itemka na puste miejsce

	                $(".draggable:eq(" + clicked + ")").css({ left: cx, top: cy });
	                $(".draggable:eq(" + clicked + ")").appendTo("#shieldcontainer");
	                alert("elo");

	                makeSpaceInBackpack();
	            } else if (!$('#shieldcontainer').is(':empty') && $(".draggable:eq(" + clicked + ")").position().left >= (cx - 10)
						&& $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)) {

	                //zamiana itemka

	                var cpI = $('.draggable:eq(' + clicked + ')').html();
	                var copiedItemFromBP = $('.draggable:eq(' + clicked + ')').clone();
	                var copiedEquiped = $('#shieldcontainer').children().clone();
	                //warunek na nie usuwanie itemka, gdy się na niego kliknie
	                if ($("#shieldcontainer").children().html() !== copiedItemFromBP.html()) {
	                    //zamiana itemek
	                    if (clickedBox !== -1) {
	                        //boost statów -> sprawdzenie czy jest założona itemka, bo sprawdzanie wyżej nie działa XD
	                        var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                        var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                        var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                        var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                        var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                        var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                        if ($("#shieldcontainer").children().attr("itemname") !== undefined) {
	                            // założony itemek
	                            $("#strvalue").html(parseInt($("#strvalue").html()) - parseInt($("#shieldcontainer").children().attr("strbonus"))+draggedStrBonus);
	                            $("#agivalue").html(parseInt($("#agivalue").html()) - parseInt($("#shieldcontainer").children().attr("agibonus"))+draggedAgiBonus);
	                            $("#vitvalue").html(parseInt($("#vitvalue").html()) - parseInt($("#shieldcontainer").children().attr("vitbonus"))+draggedVitBonus);
	                            $("#dexvalue").html(parseInt($("#dexvalue").html()) - parseInt($("#shieldcontainer").children().attr("dexbonus"))+draggedDexBonus);
	                            $("#intvalue").html(parseInt($("#intvalue").html()) - parseInt($("#shieldcontainer").children().attr("intbonus")) + draggedIntBonus);
	                            $("#lukvalue").html(parseInt($("#lukvalue").html()) - parseInt($("#shieldcontainer").children().attr("lukbonus")) + draggedLukBonus);

                                //tu jeszcze jakiś ajax do bazy

	                        } else {
	                            // włożenie itemka w puste miejsce
	                            $("#strvalue").html(parseInt($("#strvalue").html()) + draggedStrBonus);
	                            $("#agivalue").html(parseInt($("#agivalue").html()) + draggedAgiBonus);
	                            $("#vitvalue").html(parseInt($("#vitvalue").html()) + draggedVitBonus);
	                            $("#dexvalue").html(parseInt($("#dexvalue").html()) + draggedDexBonus);
	                            $("#intvalue").html(parseInt($("#intvalue").html()) + draggedIntBonus);
	                            $("#lukvalue").html(parseInt($("#lukvalue").html()) + draggedLukBonus);
	                            // i tu ajax do bazy
	                            $.ajax({
	                                type: 'POST',
	                                url: $("#emptySpace").data('request-url'),
	                                data: 'id=' + parseInt($('.draggable:eq(' + clicked + ')').attr('iditem')),
	                                success: function (data) {
	                                    //lul
	                                },
	                                error: function (data) {
	                                    //lul2
	                                }
	                            });
	                        }

	                        
	                        backpackPropertiesArr[clickedBox].empty = true;
	                        $("#shieldcontainer").children().appendTo("#littlehelper");
	                        $(".box:eq(" + clickedBox + ")").children().appendTo("#shieldcontainer");
	                        $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                        $(".box:eq(" + clickedBox + ")").children().css({ left: 0, top: 0 });
	                        $("#shieldcontainer").children().css({ left: 0, top: 0 });
	                        backpackPropertiesArr[clickedBox].empty = false;

	                        
	                    } else {
	                        $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                    }

	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                }
	            } else {
	                //może tu zrobię wsadzanie do plecaka xD
	                var posX = $(".draggable:eq(" + clicked + ")").position().left;
	                var posY = $(".draggable:eq(" + clicked + ")").position().top;
	                for (var i = 0; i < backpackPropertiesArr.length; i++) {

	                    //jeśli trzymany itemek znajduje się nad którymś divem z klasą box...
	                    if (posX >= backpackPropertiesArr[i].x &&
                                posY >= backpackPropertiesArr[i].y &&
                                posX <= (backpackPropertiesArr[i].x + hw) &&
                                posY <= (backpackPropertiesArr[i].y + hh)) {
	                        //... jeśli pozycja któregoś diva z klasą box spełnia warunek i jeśli dany div jest pusty...
	                        if (parseInt(backpackPropertiesArr[i].x) === parseInt($(".box:eq(" + i + ")").position().left) &&
                                    parseInt(backpackPropertiesArr[i].y) === parseInt($(".box:eq(" + i + ")").position().top) &&
                                    backpackPropertiesArr[i].empty) {
	                            //... jeśli rodzic przeciąganego itemka ma klasę box...
	                            if ($(".draggable:eq(" + dragged + ")").parent().hasClass("box")) {
	                                //wstaw w itemek w puste miejsce
	                                backpackPropertiesArr[clickedBox].empty = true;
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //odejmij staty z boosta
	                                var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                                var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                                var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                                var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                                var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                                var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                                $("#strvalue").html(parseInt($("#strvalue").html()) - draggedStrBonus);
	                                $("#agivalue").html(parseInt($("#agivalue").html()) - draggedAgiBonus);
	                                $("#vitvalue").html(parseInt($("#vitvalue").html()) - draggedVitBonus);
	                                $("#dexvalue").html(parseInt($("#dexvalue").html()) - draggedDexBonus);
	                                $("#intvalue").html(parseInt($("#intvalue").html()) - draggedIntBonus);
	                                $("#lukvalue").html(parseInt($("#lukvalue").html()) - draggedLukBonus);
	                                //tu ajax do bazy

	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                    }
	                }
	            }
	        } else if ($(".draggable:eq(" + dragged + ")").hasClass("legs")) {

	            var cx = $("#legscontainer").position().left + 2;
	            var cy = $("#legscontainer").position().top + 2;
	            var hw = $("#legscontainer").width();
	            var hh = $("#legscontainer").height();
	            if ($(".draggable:eq(" + clicked + ")").position().left >= (cx - 10) && $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)
						&& $('#legscontainer').is(':empty')) {

	                //wsadzenie itemka na puste miejsce

	                $(".draggable:eq(" + clicked + ")").css({ left: cx, top: cy });
	                $(".draggable:eq(" + clicked + ")").appendTo("#legscontainer");

	                makeSpaceInBackpack();
	                //alert(backpackPropertiesArr[0].empty);
	            } else if (!$('#legscontainer').is(':empty') && $(".draggable:eq(" + clicked + ")").position().left >= (cx - 10)
						&& $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)) {

	                //zamiana itemka

	                var cpI = $('.draggable:eq(' + clicked + ')').html();
	                var copiedItemFromBP = $('.draggable:eq(' + clicked + ')').clone();
	                var copiedEquiped = $('#legscontainer').children().clone();
	                //warunek na nie usuwanie itemka, gdy się na niego kliknie
	                if ($("#legscontainer").children().html() !== copiedItemFromBP.html()) {
	                    //zamiana itemek
	                    if (clickedBox !== -1) {

	                        //boost statów -> sprawdzenie czy jest założona itemka, bo sprawdzanie wyżej nie działa XD
	                        var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                        var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                        var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                        var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                        var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                        var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                        if ($("#legscontainer").children().attr("itemname") !== undefined) {
	                            // założony itemek
	                            $("#strvalue").html(parseInt($("#strvalue").html()) - parseInt($("#legscontainer").children().attr("strbonus")) + draggedStrBonus);
	                            $("#agivalue").html(parseInt($("#agivalue").html()) - parseInt($("#legscontainer").children().attr("agibonus")) + draggedAgiBonus);
	                            $("#vitvalue").html(parseInt($("#vitvalue").html()) - parseInt($("#legscontainer").children().attr("vitbonus")) + draggedVitBonus);
	                            $("#dexvalue").html(parseInt($("#dexvalue").html()) - parseInt($("#legscontainer").children().attr("dexbonus")) + draggedDexBonus);
	                            $("#intvalue").html(parseInt($("#intvalue").html()) - parseInt($("#legscontainer").children().attr("intbonus")) + draggedIntBonus);
	                            $("#lukvalue").html(parseInt($("#lukvalue").html()) - parseInt($("#legscontainer").children().attr("lukbonus")) + draggedLukBonus);

	                            //tu jeszcze jakiś ajax do bazy

	                        } else {
	                            // włożenie itemka w puste miejsce
	                            $("#strvalue").html(parseInt($("#strvalue").html()) + draggedStrBonus);
	                            $("#agivalue").html(parseInt($("#agivalue").html()) + draggedAgiBonus);
	                            $("#vitvalue").html(parseInt($("#vitvalue").html()) + draggedVitBonus);
	                            $("#dexvalue").html(parseInt($("#dexvalue").html()) + draggedDexBonus);
	                            $("#intvalue").html(parseInt($("#intvalue").html()) + draggedIntBonus);
	                            $("#lukvalue").html(parseInt($("#lukvalue").html()) + draggedLukBonus);
	                            // i tu ajax do bazy
	                            $.ajax({
	                                type: 'POST',
	                                url: $("#emptySpace").data('request-url'),
	                                data: 'id=' + parseInt($('.draggable:eq(' + clicked + ')').attr('iditem')),
	                                success: function (data) {
	                                    //lul
	                                },
	                                error: function (data) {
	                                    //lul2
	                                }
	                            });
	                        }

	                        backpackPropertiesArr[clickedBox].empty = true;
	                        $("#legscontainer").children().appendTo("#littlehelper");
	                        $(".box:eq(" + clickedBox + ")").children().appendTo("#legscontainer");
	                        $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                        $(".box:eq(" + clickedBox + ")").children().css({ left: 0, top: 0 });
	                        $("#legscontainer").children().css({ left: 0, top: 0 });
	                        backpackPropertiesArr[clickedBox].empty = false;
	                    } else {
	                        $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                    }

	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                }
	            } else {
	                //może tu zrobię wsadzanie do plecaka xD
	                var posX = $(".draggable:eq(" + dragged + ")").position().left;
	                var posY = $(".draggable:eq(" + dragged + ")").position().top;
	                for (var i = 0; i < backpackPropertiesArr.length; i++) {

	                    //jeśli trzymany itemek znajduje się nad którymś divem z klasą box...
	                    if (posX >= backpackPropertiesArr[i].x &&
                                posY >= backpackPropertiesArr[i].y &&
                                posX <= (backpackPropertiesArr[i].x + hw) &&
                                posY <= (backpackPropertiesArr[i].y + hh)) {
	                        //... jeśli pozycja któregoś diva z klasą box spełnia warunek i jeśli dany div jest pusty...
	                        if (parseInt(backpackPropertiesArr[i].x) === parseInt($(".box:eq(" + i + ")").position().left) &&
                                    parseInt(backpackPropertiesArr[i].y) === parseInt($(".box:eq(" + i + ")").position().top) &&
                                    backpackPropertiesArr[i].empty) {

	                            //... jeśli rodzic przeciąganego itemka ma klasę box...
	                            if ($(".draggable:eq(" + dragged + ")").parent().hasClass("box")) {
	                                //wstaw w itemek w puste miejsce
	                                backpackPropertiesArr[clickedBox].empty = true;
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //odejmij staty z boosta
	                                var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                                var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                                var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                                var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                                var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                                var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                                $("#strvalue").html(parseInt($("#strvalue").html()) - draggedStrBonus);
	                                $("#agivalue").html(parseInt($("#agivalue").html()) - draggedAgiBonus);
	                                $("#vitvalue").html(parseInt($("#vitvalue").html()) - draggedVitBonus);
	                                $("#dexvalue").html(parseInt($("#dexvalue").html()) - draggedDexBonus);
	                                $("#intvalue").html(parseInt($("#intvalue").html()) - draggedIntBonus);
	                                $("#lukvalue").html(parseInt($("#lukvalue").html()) - draggedLukBonus);
	                                //tu ajax do bazy

	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                    }
	                }
	            }
	        } else if ($(".draggable:eq(" + dragged + ")").hasClass("accessory")) {

	            var cx = $("#accessorycontainer").position().left + 2;
	            var cy = $("#accessorycontainer").position().top + 2;
	            var hw = $("#accessorycontainer").width();
	            var hh = $("#accessorycontainer").height();
	            if ($(".draggable:eq(" + clicked + ")").position().left >= (cx - 10) && $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)
						&& $('#accessorycontainer').is(':empty')) {

	                //wsadzenie itemka na puste miejsce

	                $(".draggable:eq(" + clicked + ")").css({ left: cx, top: cy });
	                $(".draggable:eq(" + clicked + ")").appendTo("#accessorycontainer");

	                makeSpaceInBackpack();
	                //alert(backpackPropertiesArr[0].empty);
	            } else if (!$('#accessorycontainer').is(':empty') && $(".draggable:eq(" + clicked + ")").position().left >= (cx - 10)
						&& $(".draggable:eq(" + clicked + ")").position().top >= (cy - 10)
						&& $(".draggable:eq(" + clicked + ")").position().left <= (cx + hw + 10) && $(".draggable:eq(" + clicked + ")").position().top <= (cy + hh + 10)) {

	                //zamiana itemka

	                var cpI = $('.draggable:eq(' + clicked + ')').html();
	                var copiedItemFromBP = $('.draggable:eq(' + clicked + ')').clone();
	                var copiedEquiped = $('#accessorycontainer').children().clone();
	                //warunek na nie usuwanie itemka, gdy się na niego kliknie
	                if ($("#accessorycontainer").children().html() !== copiedItemFromBP.html()) {
	                    //zamiana itemek
	                    if (clickedBox !== -1) {

	                        //boost statów -> sprawdzenie czy jest założona itemka, bo sprawdzanie wyżej nie działa XD
	                        var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                        var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                        var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                        var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                        var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                        var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                        if ($("#accessorycontainer").children().attr("itemname") !== undefined) {
	                            // założony itemek
	                            $("#strvalue").html(parseInt($("#strvalue").html()) - parseInt($("#accessorycontainer").children().attr("strbonus")) + draggedStrBonus);
	                            $("#agivalue").html(parseInt($("#agivalue").html()) - parseInt($("#accessorycontainer").children().attr("agibonus")) + draggedAgiBonus);
	                            $("#vitvalue").html(parseInt($("#vitvalue").html()) - parseInt($("#accessorycontainer").children().attr("vitbonus")) + draggedVitBonus);
	                            $("#dexvalue").html(parseInt($("#dexvalue").html()) - parseInt($("#accessorycontainer").children().attr("dexbonus")) + draggedDexBonus);
	                            $("#intvalue").html(parseInt($("#intvalue").html()) - parseInt($("#accessorycontainer").children().attr("intbonus")) + draggedIntBonus);
	                            $("#lukvalue").html(parseInt($("#lukvalue").html()) - parseInt($("#shieldcontainer").children().attr("lukbonus")) + draggedLukBonus);

	                            //tu jeszcze jakiś ajax do bazy

	                        } else {
	                            // włożenie itemka w puste miejsce
	                            $("#strvalue").html(parseInt($("#strvalue").html()) + draggedStrBonus);
	                            $("#agivalue").html(parseInt($("#agivalue").html()) + draggedAgiBonus);
	                            $("#vitvalue").html(parseInt($("#vitvalue").html()) + draggedVitBonus);
	                            $("#dexvalue").html(parseInt($("#dexvalue").html()) + draggedDexBonus);
	                            $("#intvalue").html(parseInt($("#intvalue").html()) + draggedIntBonus);
	                            $("#lukvalue").html(parseInt($("#lukvalue").html()) + draggedLukBonus);
	                            // i tu ajax do bazy
	                            $.ajax({
	                                type: 'POST',
	                                url: $("#emptySpace").data('request-url'),
	                                data: 'id=' + parseInt($('.draggable:eq(' + clicked + ')').attr('iditem')),
	                                success: function (data) {
	                                    //lul
	                                },
	                                error: function (data) {
	                                    //lul2
	                                }
	                            });
	                        }

	                        backpackPropertiesArr[clickedBox].empty = true;
	                        $("#accessorycontainer").children().appendTo("#littlehelper");
	                        $(".box:eq(" + clickedBox + ")").children().appendTo("#accessorycontainer");
	                        $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                        $(".box:eq(" + clickedBox + ")").children().css({ left: 0, top: 0 });
	                        $("#accessorycontainer").children().css({ left: 0, top: 0 });
	                        backpackPropertiesArr[clickedBox].empty = false;
	                    } else {
	                        $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                    }

	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                }
	            } else {
	                //może tu zrobię wsadzanie do plecaka xD
	                var posX = $(".draggable:eq(" + dragged + ")").position().left;
	                var posY = $(".draggable:eq(" + dragged + ")").position().top;
	                for (var i = 0; i < backpackPropertiesArr.length; i++) {

	                    //jeśli trzymany itemek znajduje się nad którymś divem z klasą box...
	                    if (posX >= backpackPropertiesArr[i].x &&
                                posY >= backpackPropertiesArr[i].y &&
                                posX <= (backpackPropertiesArr[i].x + hw) &&
                                posY <= (backpackPropertiesArr[i].y + hh)) {
	                        //... jeśli pozycja któregoś diva z klasą box spełnia warunek i jeśli dany div jest pusty...
	                        if (parseInt(backpackPropertiesArr[i].x) === parseInt($(".box:eq(" + i + ")").position().left) &&
                                    parseInt(backpackPropertiesArr[i].y) === parseInt($(".box:eq(" + i + ")").position().top) &&
                                    backpackPropertiesArr[i].empty) {

	                            //... jeśli rodzic przeciąganego itemka ma klasę box...
	                            if ($(".draggable:eq(" + dragged + ")").parent().hasClass("box")) {
	                                //wstaw w itemek w puste miejsce
	                                backpackPropertiesArr[clickedBox].empty = true;
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //odejmij staty z boosta
	                                var draggedStrBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("strbonus"));
	                                var draggedAgiBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("agibonus"));
	                                var draggedVitBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("vitbonus"));
	                                var draggedDexBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("dexbonus"));
	                                var draggedIntBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("intbonus"));
	                                var draggedLukBonus = parseInt($('.draggable:eq(' + clicked + ')').attr("lukbonus"));
	                                $("#strvalue").html(parseInt($("#strvalue").html()) - draggedStrBonus);
	                                $("#agivalue").html(parseInt($("#agivalue").html()) - draggedAgiBonus);
	                                $("#vitvalue").html(parseInt($("#vitvalue").html()) - draggedVitBonus);
	                                $("#dexvalue").html(parseInt($("#dexvalue").html()) - draggedDexBonus);
	                                $("#intvalue").html(parseInt($("#intvalue").html()) - draggedIntBonus);
	                                $("#lukvalue").html(parseInt($("#lukvalue").html()) - draggedLukBonus);
	                                //tu ajax do bazy

	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: 0, top: 0 });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: 0, top: 0 });
	                    }
	                }
	            }
	        }



	        $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	    }

	});

    function makeSpaceInBackpack() {
        for (var i = 0; i < $(".box").length; i++) {
            if (!backpackPropertiesArr[i].empty && $('.box:eq(' + i + ')').is(':empty')) {
                backpackPropertiesArr[i].empty = true;
            }
        }
    }

    $(window).resize(function () {
        setBackpackProperties();
    });

    $(window).click(function () {
        setBackpackProperties();
    });

    $(window).mousedown(function () {
        setBackpackProperties();
    });
});