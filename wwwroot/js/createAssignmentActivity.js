
document.addEventListener("DOMContentLoaded", function() {
  document.getElementById("createAssignmentForm").addEventListener("submit", function(event) {
    event.preventDefault();
    let courseId=document.getElementById("form-input-course-id").value;
  

      var formData = new FormData();
      formData.append("Name", document.getElementById("form-input-name").value);
      formData.append("Description", document.getElementById("form-input-description").value);
      formData.append("PassingGrade", document.getElementById("form-input-passing-grade").value);
      formData.append("StartDate", document.getElementById("form-input-start-date").value);
      formData.append("EndDate", document.getElementById("form-input-end-date").value);
      formData.append("CourseId", document.getElementById("form-input-course-id").value);

      // create new XMLHttpRequest
      var xhr = new XMLHttpRequest();

      // config request
      xhr.open("POST", "/activity/assignment/create", true);

      // complete request
      xhr.onload = function () {
          if (xhr.status >= 200 && xhr.status < 300) {
            var response = JSON.parse(xhr.responseText);
            if (response.status === "error") {
              //"error"
              Toastify({
                text: response.message,
                backgroundColor: 'linear-gradient(to right, #ff6e7f, #bfe9ff)',
              }).showToast();
            } else {
              // "success"
              Toastify({
                text: response.message,
                backgroundColor: 'linear-gradient(to right, #00b09b, #96c93d)',
              }).showToast();
              window.location.href = `/course/${courseId}`;
            }
          } else {
            Toastify({
              text: 'Đã có lỗi xảy ra trong quá trình gửi request.',
              backgroundColor: 'linear-gradient(to right, #ff6e7f, #bfe9ff)',
            }).showToast();
          }
      };

      xhr.onerror = function () {
          console.error("Đã có lỗi xảy ra trong quá trình gửi request.");
      };

      xhr.send(formData);
  });
});
