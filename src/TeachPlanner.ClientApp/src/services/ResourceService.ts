import { baseUrl } from "../utils/constants";

class ResourceService {
  async getResources(subjectId: string, teacherId: string, token: string): Promise<Resource[]> {
    const abortController = new AbortController();

    const response = await fetch(`${baseUrl}/${teacherId}/resources/${subjectId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      signal: abortController.signal,
    });

    const resources = await response.json();
    return resources;
  }
}

export default new ResourceService();
