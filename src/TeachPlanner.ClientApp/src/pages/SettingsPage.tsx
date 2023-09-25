import { useEffect, useState } from "react";
import Dropdown from "../components/common/Dropdown";
import teacherService from "../services/TeacherService";
import useAuth from "../contexts/AuthContext";
import Button from "../components/common/Button";

type SubjectIdentifier = {
  id: string;
  name: string;
};

const SettingsPage = () => {
  const [subjects, setSubjects] = useState<SubjectIdentifier[]>([]);
  const [subjectsTaught, setSubjectsTaught] = useState<SubjectIdentifier[]>([]);
  const { teacher, token } = useAuth();

  useEffect(() => {
    const getSubjects = async () => {
      const abortController = new AbortController();
      const settings = await teacherService.getSettingsData(teacher!, 2023, token!);

      setSubjects(settings.curriculumSubjects);
      setSubjectsTaught(settings.subjectsTaught);
    };

    getSubjects();
  }, []);

  async function handleSetSubjects() {
    try {
      await teacherService.setSubjectsTaught(
        teacher!,
        token!,
        subjectsTaught.map((s) => s.id),
        2023
      );
      console.log("Subjects taught successfully set");
    } catch (error) {
      console.log(error);
    }
  }

  function handleSubjectSelect(subjectsToAdd: string[]) {
    const subjectIdentifiers = subjects.filter((sub) => subjectsToAdd.find((s) => s === sub.name));

    setSubjectsTaught(subjectIdentifiers);
  }
  return (
    <div className="flex-grow">
      <h1>Settings</h1>
      <Dropdown
        placeholder="choose taught subjects"
        multiSelect={true}
        options={subjects.map((subject) => subject.name)}
        onChange={handleSubjectSelect}
      />
      <Button variant="submit" onClick={handleSetSubjects}>
        Submit
      </Button>
    </div>
  );
};
export default SettingsPage;
