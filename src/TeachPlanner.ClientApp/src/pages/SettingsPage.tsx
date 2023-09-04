import { useEffect, useState } from "react";
import Dropdown from "../components/common/Dropdown";
import curriculumService from "../services/CurriculumService";
import teacherService from "../services/TeacherService";
import useAuth from "../contexts/AuthContext";
import Button from "../components/common/Button";

const SettingsPage = () => {
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const [subjectsTaught, setSubjectsTaught] = useState<string[]>([]);
  const { teacher } = useAuth();

  useEffect(() => {
    const getSubjects = async () => {
      const abortController = new AbortController();
      const subjects = await curriculumService.getSubjects({}, teacher!, abortController);

      setSubjects(subjects.subjects);
    };

    getSubjects();
  }, []);

  async function handleSetSubjects() {
    try {
      await teacherService.setSubjectsTaught(teacher!, subjectsTaught);
      console.log("Subjects taught successfully set");
    } catch (error) {
      console.log(error);
    }
  }

  function handleSubjectSelect(subjects: string[]) {
    setSubjectsTaught(subjects);
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
