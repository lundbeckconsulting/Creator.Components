const toggleVisible = (tag) {
    if (tag.classList.contains("hide")) {
        tag.classList.remove("hide");
    }
    else {
        tag.classList.add("hide");
    }
};
