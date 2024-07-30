const modal = document.getElementById("enroll-user-modal");
const input = document.getElementById("enroll-user-select");
const confirmButton = document.getElementById("enroll-confirm");

modal.addEventListener("show.bs.modal", async (event) => {
	const button = event.relatedTarget;
	const role = button.getAttribute("data-role");
	const courseId = button.getAttribute("data-course-id");

	const users = await (fetch(`/course/${courseId}/unenrolledUsers`)
		.then((response) => response.json()));

	input.innerHTML = "";

	for (const { id, name, email } of users) {
		const node = document.createElement("option");
		node.innerText = `${name} (${email})`;
		node.value = id;
		input.append(node);
	}

	confirmButton.onclick = async () => {
		confirmButton.disabled = true;

		const data = new FormData();
		data.set("UserId", input.value);
		data.set("Role", role);

		await (fetch(`/course/${courseId}/enroll`, {
			method: "POST",
			body: data
		}).then((response) => response.json()));

		location.reload();
	}
});
