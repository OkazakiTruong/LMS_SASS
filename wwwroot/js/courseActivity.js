// Assuming courseId is known
var courseId = document.getElementById('form-input-course-id').value;
var listActivitiesContainer = document.getElementById('list-activities');
var url = `/course/${courseId}/activities?orderBy=startDate&sortBy=desc`;
// Create a new XMLHttpRequest object
var xhr = new XMLHttpRequest();

// Configure the request
xhr.open('GET',url, true);
xhr.setRequestHeader('Content-Type', 'application/json');

// Set up a callback function to handle the response
xhr.onload = function() {
    if (xhr.status >= 200 && xhr.status < 300) {
        // Request was successful
        var data = JSON.parse(xhr.responseText);
        data.forEach(function(activity) {
          var activityDiv = document.createElement('div');
          activityDiv.classList.add('activity');
          var iconElement = document.createElement('i');
          var nameLink = document.createElement('a');
          if (activity.type === 'meeting') {
            iconElement.classList.add('fa-solid', 'fa-video');
        } else if (activity.type === 'assignment') {
            iconElement.classList.add('fa-regular', 'fa-file');
        } else if (activity.type === 'quiz') {
            iconElement.classList.add('fa-regular', 'fa-circle-check');
        } else {
            // Default icon if type is not recognized
            iconElement.classList.add('fa-solid', 'fa-question-circle');
        }

          nameLink.href = '#';
          var typeSpan = document.createElement('span');
          typeSpan.textContent = activity.name;
          activityDiv.appendChild(nameLink);
          nameLink.appendChild(iconElement);
          nameLink.appendChild(typeSpan);

          nameLink.classList.add('btn', 'btn-outline', 'btn-outline-secondary', 'w-100', 'm-4', 'p-4');
      

      


      
          // Append the activity div to the container
          listActivitiesContainer.appendChild(activityDiv);
      });
    } else {
        // Request failed
        console.error('Request failed with status:', xhr.status);
    }
};

// Set up a callback function to handle errors
xhr.onerror = function() {
    // Handle any errors that occur during the request
    console.error('Request failed');
};

// Send the request
xhr.send();