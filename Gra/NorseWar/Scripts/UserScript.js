$(function () {
    var clicked;
    var clickedBox;
    var backpackPropertiesArr = new Array();

    setBackpackProperties();

    function setBackpackProperties() {
        backpackPropertiesArr = new Array();
        for (var i = 0; i < $(".box").length; i++) {
            if ($(".box:eq(" + i + ")").is(':empty')) {
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
	                    backpackPropertiesArr[clickedBox].empty = true;
	                    $("#helmetcontainer").children().appendTo("#littlehelper");
	                    $(".box:eq(" + clickedBox + ")").children().appendTo("#helmetcontainer");
	                    $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                    $(".box:eq(" + clickedBox + ")").children().css({ left: $(".box:eq(" + clickedBox + ")").position().left, top: $(".box:eq(" + clickedBox + ")").position().top });
	                    $("#helmetcontainer").children().css({ left: $("#helmetcontainer").position().left, top: $("#helmetcontainer").position().top });
	                    backpackPropertiesArr[clickedBox].empty = false;
	                    $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                    $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
	                    backpackPropertiesArr[clickedBox].empty = true;
	                    $("#weaponcontainer").children().appendTo("#littlehelper");
	                    $(".box:eq(" + clickedBox + ")").children().appendTo("#weaponcontainer");
	                    $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                    $(".box:eq(" + clickedBox + ")").children().css({ left: $(".box:eq(" + clickedBox + ")").position().left, top: $(".box:eq(" + clickedBox + ")").position().top });
	                    $("#weaponcontainer").children().css({ left: $("#weaponcontainer").position().left, top: $("#weaponcontainer").position().top });
	                    backpackPropertiesArr[clickedBox].empty = false;
	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
	                    backpackPropertiesArr[clickedBox].empty = true;
	                    $("#bootscontainer").children().appendTo("#littlehelper");
	                    $(".box:eq(" + clickedBox + ")").children().appendTo("#bootscontainer");
	                    $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                    $(".box:eq(" + clickedBox + ")").children().css({ left: $(".box:eq(" + clickedBox + ")").position().left, top: $(".box:eq(" + clickedBox + ")").position().top });
	                    $("#bootscontainer").children().css({ left: $("#bootscontainer").position().left, top: $("#bootscontainer").position().top });
	                    backpackPropertiesArr[clickedBox].empty = false;
	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
	                        backpackPropertiesArr[clickedBox].empty = true;
	                        $("#armorcontainer").children().appendTo("#littlehelper");
	                        $(".box:eq(" + clickedBox + ")").children().appendTo("#armorcontainer");
	                        $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                        $(".box:eq(" + clickedBox + ")").children().css({ left: $(".box:eq(" + clickedBox + ")").position().left, top: $(".box:eq(" + clickedBox + ")").position().top });
	                        $("#armorcontainer").children().css({ left: $("#armorcontainer").position().left, top: $("#armorcontainer").position().top });
	                        backpackPropertiesArr[clickedBox].empty = false;
	                    } else {
	                        $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
	                    }

	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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

	                makeSpaceInBackpack();
	                //alert(backpackPropertiesArr[0].empty);
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
	                        backpackPropertiesArr[clickedBox].empty = true;
	                        $("#shieldcontainer").children().appendTo("#littlehelper");
	                        $(".box:eq(" + clickedBox + ")").children().appendTo("#shieldcontainer");
	                        $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                        $(".box:eq(" + clickedBox + ")").children().css({ left: $(".box:eq(" + clickedBox + ")").position().left, top: $(".box:eq(" + clickedBox + ")").position().top });
	                        $("#shieldcontainer").children().css({ left: $("#shieldcontainer").position().left, top: $("#shieldcontainer").position().top });
	                        backpackPropertiesArr[clickedBox].empty = false;
	                    } else {
	                        $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
	                    }

	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
	                        backpackPropertiesArr[clickedBox].empty = true;
	                        $("#legscontainer").children().appendTo("#littlehelper");
	                        $(".box:eq(" + clickedBox + ")").children().appendTo("#legscontainer");
	                        $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                        $(".box:eq(" + clickedBox + ")").children().css({ left: $(".box:eq(" + clickedBox + ")").position().left, top: $(".box:eq(" + clickedBox + ")").position().top });
	                        $("#legscontainer").children().css({ left: $("#legscontainer").position().left, top: $("#legscontainer").position().top });
	                        backpackPropertiesArr[clickedBox].empty = false;
	                    } else {
	                        $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
	                    }

	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
	                        backpackPropertiesArr[clickedBox].empty = true;
	                        $("#accessorycontainer").children().appendTo("#littlehelper");
	                        $(".box:eq(" + clickedBox + ")").children().appendTo("#accessorycontainer");
	                        $("#littlehelper").children().appendTo(".box:eq(" + clickedBox + ")");
	                        $(".box:eq(" + clickedBox + ")").children().css({ left: $(".box:eq(" + clickedBox + ")").position().left, top: $(".box:eq(" + clickedBox + ")").position().top });
	                        $("#accessorycontainer").children().css({ left: $("#accessorycontainer").position().left, top: $("#accessorycontainer").position().top });
	                        backpackPropertiesArr[clickedBox].empty = false;
	                    } else {
	                        $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
	                    }

	                } else {
	                    $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            } else {
	                                //jeśli itemek znajdował się w ekwipunku, następuje wstawienie go na puste miejsce
	                                $(".draggable:eq(" + dragged + ")").appendTo($(".box:eq(" + i + ")"));
	                                $(".box:eq(" + i + ")").children().css({ left: backpackPropertiesArr[i].x, top: backpackPropertiesArr[i].y });
	                                backpackPropertiesArr[i].empty = false;
	                                $(".draggable:eq(" + clicked + ")").css({ "zIndex": "1" });
	                                return;
	                            }
	                        }
	                    } else {
	                        //jeśli itemek zostanie upuszczony na randomowej pozycji, jest ustawiany na miejsce swojego rodzica
	                        $(".draggable:eq(" + clicked + ")").css({ left: $(this).parent().position().left, top: $(this).parent().position().top });
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
});