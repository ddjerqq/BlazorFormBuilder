export const AddEventHandlersToDraggableContainers = () => {
  document.querySelectorAll("[ondragoverPreventDefault=true]")
    .forEach(el => el.addEventListener("dragover", e => e.preventDefault()));
}