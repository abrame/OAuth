$(document).ready(function () {

  $("#getCalendars").submit(function () {
    // get access token
    var token = $("#token").val();

    if (token.length > 0) {

      $.ajax("https://www.googleapis.com/calendar/v3/users/me/calendarList",
        {
          headers: { Authorization: "Bearer " + token }

        }).done(done_getCalendars).fail(fail_getCalendars);
    }
    return false;
  });

  $("#getCalendar").submit(function () {
    // get access token
    var token = $("#token").val();
    var calID = encodeURI($("#getCalID").val());

    if (token.length > 0 && calID.length > 0) {
      var uri = "https://www.googleapis.com/calendar/v3/calendars/" + calID;

      $.ajax(uri,
        {
          headers: { Authorization: "Bearer " + token }

        }).done(done_getCal).fail(fail_getCal);
    }

    return false;
  });

  $("#getEvents").submit(function () {
    // get access token
    var token = $("#token").val();
    var calID = $("#getEventsCalID").val();

    if (token.length > 0 && calID.length > 0) {
      var uri = "https://www.googleapis.com/calendar/v3/calendars/" + calID + "/events";

      $.ajax(uri,
        {
          headers: { Authorization: "Bearer " + token }

        }).done(done_getEvents).fail(fail_getEvents);
    }

    return false;
  });

  $("#addEvent").submit(function () {
    // get access token
    var token = $("#token").val();
    var calID = $("#addEventCalID").val();
    var startTime = $("#addEvent input[name=start]").val();
    startTime = new Date(2013, 8, 4, 18);
    var endTime = $("#addEvent input[name=end]").val();
    endTime = new Date(2013, 8, 4, 19);

    if (token.length > 0 && calID.length > 0) {
      var uri = "https://www.googleapis.com/calendar/v3/calendars/" + calID + "/events";

      $.ajax(uri,
        {
          headers: { Authorization: "Bearer " + token },
          type: "POST",
          contentType: "application/json",
          data: JSON.stringify(
            {
              "start":
                {
                  "dataTime": startTime
                },
              "end":
                {
                  "dateTime": endTime
                }
            })

        }).done(done_addEvent).fail(fail_addEvent);
    }

    return false;
  });
});

function done_getCalendars(data, textStatus, jqXHR) {
  //alert("success");
  var html = ""
  if (data.items != null && data.items.length > 0) {

    for (var i = 0; i < data.items.length; i++) {
      var item = data.items[i];
      html += "<div>ID: " + item.id;
      if (item.summary != null) html += "<br />Summary: " + item.summary;
      if (item.description != null) html += "<br />Description: " + item.description;
      html += "</div><br />";
    }

  }
  else {
    html = "No calendars available";
  }

  $("#data").html(html);
}

function fail_getCalendars(jqXHR, textStatus, errorThrown) {
  alert(errorThrown);
}

function done_getCal(data, textStatus, jqXHR) {
  var html = ""
  html += "<div>ID: " + data.id;
  if (data.summary != null) html += "<br />Summary: " + data.summary;
  if (data.description != null) html += "<br />Description: " + data.description;
  html += "</div>";

  $("#data").html(html);
}

function fail_getCal(jqXHR, textStatus, errorThrown) {
  alert("failure");
}

function done_getEvents(data, textStatus, jqXHR) {
  //alert("success");
  var html = ""
  if (data.items != null && data.items.length > 0) {

    for (var i = 0; i < data.items.length; i++) {
      var item = data.items[i];
      html += "<div>Event ID: " + item.id;
      if (item.summary != null) html += "<br />Summary: " + item.summary;
      if (item.start != null) html += "<br />Start: " + item.start.dateTime;
      if (item.end != null) html += "<br />End: " + item.end.dateTime;
      html += "</div><br />";
    }

  }
  else {
    html = "No calendars available";
  }

  $("#data").html(html);
}

function fail_getEvents(jqXHR, textStatus, errorThrown) {
  alert("failure");
}

function done_addEvent(data, textStatus, jqXHR) {
  alert("success");
}

function fail_addEvent(jqXHR, textStatus, errorThrown) {
  alert("failure");
}