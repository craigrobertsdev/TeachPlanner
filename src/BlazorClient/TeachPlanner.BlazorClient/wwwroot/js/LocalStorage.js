async function loadStateFromLocalStorage() {
    var state = await localStorage.getItem("teacherId");

    if (state === null) {
        console.log("No state found in local storage");
        return null;
    }

    return state;
}