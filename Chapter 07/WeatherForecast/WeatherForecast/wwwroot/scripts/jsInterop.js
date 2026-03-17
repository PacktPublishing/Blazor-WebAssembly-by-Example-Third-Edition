export async function getPosition() {
    if (!navigator.geolocation) {
        throw new Error("Geolocation is not supported.");
    }

    const position = await new Promise((resolve, reject) => {
        navigator.geolocation.getCurrentPosition(resolve, reject);
    });

    return {
        latitude: position.coords.latitude,
        longitude: position.coords.longitude
    };
}