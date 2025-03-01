export const SetupDragEvents = (dotnetHelper) => {
  let currentTouch = null;

  const onTouchMove = (event) => {
    event.preventDefault();
    currentTouch = event.touches[0];

    const dropzones = document.querySelectorAll('.drop-zone');
    dropzones.forEach((zone) => {
      const rect = zone.getBoundingClientRect();
      const touchX = currentTouch.clientX;
      const touchY = currentTouch.clientY;

      if (touchX >= rect.left && touchX <= rect.right &&
        touchY >= rect.top && touchY <= rect.bottom) {
        zone.classList.add('active-dropzone');
      } else {
        zone.classList.remove('active-dropzone');
      }
    });
  };

  const onTouchEnd = (event) => {
    event.preventDefault();
    let dropIndex = null;
    const dropzones = document.querySelectorAll('.drop-zone');
    dropzones.forEach((zone, index) => {
      if (zone.classList.contains('active-dropzone')) {
        dropIndex = index;
      }
      zone.classList.remove('active-dropzone');
    });

    if (dropIndex !== null) {
      dotnetHelper.invokeMethodAsync("DropFromJs", dropIndex);
    }
  };

  const initDraggables = () => {
    const draggables = document.querySelectorAll('[draggable="true"]');
    draggables.forEach((el) => {
      el.addEventListener('touchmove', onTouchMove, { passive: false });
      el.addEventListener('touchend', onTouchEnd, { passive: false });
    });
  };

  if (document.readyState !== 'loading') {
    initDraggables();
  } else {
    document.addEventListener('DOMContentLoaded', initDraggables);
  }
};