$( document ).ready(function() {
    // Uncomment this line to change the language to english
    moment.locale('en')
    let now = moment().startOf('month')
    console.log(now.format('MMMM Do YYYY, h:mm:ss a d'))

    let WeekOrder = moment(now)
    let dayDiff = WeekOrder.startOf('week')
    WeekOrder.subtract(dayDiff,'days')
    
    // Create the table headers
    for (let i = 0; i < 7; i++) {

        let dayText = WeekOrder.format('dd') 
        let th = document.createElement("th")
        th.innerHTML= dayText.charAt(0)

        $('#calendar-head').append(th)

        WeekOrder.add(1,'day')
    }

    updateCalendar(now)

    $( "#calendar-header .fa-angle-left" ).click(() =>
    updateCalendar(now.subtract(2, 'months'))    
    )
    $( "#calendar-header .fa-angle-right" ).click(() =>
        updateCalendar(now.add(0, 'months'))
    )


    //Mail stuff
    $('.expand-mail').click((ev) => {
        if (ev.target.classList.contains('fa-angle-right')) {
            ev.target.classList.remove('fa-angle-right')
            ev.target.classList.add('fa-angle-down')
        } else {
            ev.target.classList.remove('fa-angle-down')
            ev.target.classList.add('fa-angle-right')
        }


        let element = ev.target.parentElement.parentElement.parentElement
        .querySelector("article")

        if (element.classList.contains('hidden')) {
            element.classList.remove('hidden')
        } else {
            element.classList.add('hidden')
        }
    })

    $('.delete-mail').click((ev) => {
        let element = ev.target.parentElement.parentElement.parentElement
        element.parentElement.removeChild(element)

        element.querySelector("article")
    })
    $('.expand-reminder').click((ev) => {
        if (ev.target.classList.contains('fa-close')) {
            ev.target.classList.remove('fa-close')
            ev.target.classList.add('fa-angle-down')
        } else {
            ev.target.classList.remove('fa-angle-down')
            ev.target.classList.add('fa-close')
        }


        let element = ev.target.parentElement.parentElement
        .querySelector(".expanded-reminder")

        if (element.classList.contains('hidden')) {
            element.classList.remove('hidden')
        } else {
            element.classList.add('hidden')
        }
    })

    
})

function updateCalendar(now) {

    // Set the month selector text and empty the table
    $('#calendar-header h3').html(now.format('MMMM YYYY'))
    $('#calendar-body').empty()

    let dayOfMonth = now
    let lastDay = parseInt(now.endOf('month').format('D'))

    now.startOf('month')

    let tr = document.createElement("tr")
    let lang = moment.locale()
    
    //in relation to the language, we change the day week starts(monday/sunday)
    let weekStartDay = 0
    if (moment.locale() == "en") {weekStartDay = 6}

    let dayNumber = 1
    for (let i = 0; i < lastDay; i++) {
        
        let td = document.createElement("td")

        td.innerHTML = dayNumber
        dayNumber++
        tr.append(td)

        if ( dayOfMonth.format('d') == weekStartDay ) {
            // Insert empty td to make the calendar start where it should
            while (tr.childElementCount < 7) {
                tr.insertBefore(document.createElement("td"), tr.firstChild)
            }
            $('#calendar-body').append(tr)
            tr = document.createElement("tr")
        }

        dayOfMonth.add('1','day')
    }
    $('#calendar-body').append(tr)
    
}
