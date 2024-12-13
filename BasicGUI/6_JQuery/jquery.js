// write all JS code into this so it will execute after loading entire html page/ document is ready
// $(document.ready()) == $()
$(document).ready(function () {
  console.log($);
  console.log(jQuery);
  // $: This is a shorthand or alias for jQuery and is widely used to make code shorter and more readable.
  // jQuery: This is the original name of the function and works the same as $.

  //----------------------------------------------------------------

  // 3 main type of selector:
  // 1. element selector
  // 2. class selector
  // 3. id selector

  // 1. element selector
  $("h1").click(); // click on h1
  $("p").click(function () {
    console.log("click on p.");
    // action = hide selector when it is clicked.
    // $('p').hide(); : if we use this syntex then it will hide all p tags when we click on anyone of p tag which is insufficient

    // solution:
    // $(this).hide(); // only hide selected p tag
    // console.log(this);
  });

  // 3. id selector
  // access any html tags using "id" name (use symbol: #)
  $("#p1").click(function () {
    console.log("call by id");
  });

  // 2. class selector
  // access any html tags using "class" name (use symbol: .)
  $(".p2").click(function () {
    console.log("call by class");
  });

  // other selectors:
  $("*").click(function () {
    console.log("click on all the elements");
  });

  $("p#p1").click(function () {
    console.log("click on element with id.");
  });

  $("p.p2").click(function () {
    console.log("click on element with class.");
  });

  // Events

  // Mouse events: click, dbclick, mouseenter, mouseleave, mouse, hover
  // Keyboard events: keydown, keypress, keyup
  // form events: submit, change, focus, blur
  // document/window events: load, resize, scroll, unload

  // "on" method : use to trigger multiple event on same element

  $("#p1").on({
    click: function () {
      console.log("click event fired");
    },
    mouseleave: function () {
      console.log("mouseleave event fired");
    },
  });

  // hide and slow events:
  // callback function:

  //   $("h2").hide(5000, function () {
  //     // callback function
  //     console.log("hide event fired after 5000 ms");
  //   });

  //   $("h2").show(5000, function () {
  //     console.log("slow event fired after 5000 ms");
  //   });

  // SEE (event):  toggle(with hide and show) , fadeOut, fadeIn, fadeToggle, fadeTo
  // slideDown, slideUp, slideToggle

  // events args: (timeUp, callback function(optional))

  // Animate function:
  $("#button1").click(function () {
    $("#p3").animate(
      {
        opacity: 0.5,
        color: "red", // Color should be in quotes
      },
      1000 // can use : "fast = 200ms", "slow = 600ms"
    );

    // $("#name").val("Nahii");
    // $("#name").html("First Name:"); ----> ?
    // $("#name").empty(); ----> ?
    // $("#name").text("kk"); ----> ?
    $("#name").remove(); // use for remove html element from DOM

    // for add class:
    // .addClass
    // .removeClass

    // for add css:
    // .css()
  });

  //   AJex

  //   GET request:
  $.get("./node_modules/jquery/dist/jquery.min.js", function (data, status) {
    // alert(data);
    // alert(status);
  });

  //   POST request:
  //   $.post(
  //     "/node_modules/jquery/dist/jquery.min.js",
  //     {
  //       name: "nahii",
  //       email: "nahii@gmail.com",
  //     },
  //     function (data, status) {
  //       alert(status);
  //     }
  //   );

  //   o/p: (error= post is not allowed) : jquery.js:121
  //  POST http://127.0.0.1:5500/node_modules/jquery/dist/jquery.min.js 405 (Method Not Allowed)
  //  (anonymous)	@	jquery.js:121
  //  Show 13 more frames
});
