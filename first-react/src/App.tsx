import './App.css';
import {
  TextField,
  Button,
  Typography,
  Container,
  Box,
  IconButton,
  Paper
} from "@mui/material";
import { useState, ChangeEvent } from "react";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';
import RadioButtonUncheckedIcon from '@mui/icons-material/RadioButtonUnchecked';

// Define interfaces for TypeScript
interface TodoItem {
  val: string;
  isDone: boolean;
  id: number;
}

function App() {
  const [inputVal, setInputVal] = useState<string>("");
  const [todos, setTodos] = useState<TodoItem[]>([]);
  const [isEdited, setIsEdited] = useState<boolean>(false);
  const [editedId, setEditedId] = useState<number | null>(null);

  const onChange = (e: ChangeEvent<HTMLInputElement>) => setInputVal(e.target.value);

  const handleClick = () => {
    if (!isEdited) {
      setTodos([...todos, { val: inputVal, isDone: false, id: Date.now() }]);
    } else if (editedId !== null) {
      setTodos(todos.map(todo => todo.id === editedId ? { ...todo, val: inputVal } : todo));
    }
    setInputVal("");
    setIsEdited(false);
    setEditedId(null);
  };

  const handleDelete = (id: number) => {
    setTodos(todos.filter((todo) => todo.id !== id));
  };

  const handleDone = (id: number) => {
    setTodos(todos.map(todo => todo.id === id ? { ...todo, isDone: !todo.isDone } : todo));
  };

  const handleEdit = (id: number) => {
    const todo = todos.find((todo) => todo.id === id);
    if (todo) {
      setEditedId(todo.id);
      setInputVal(todo.val);
      setIsEdited(true);
    }
  };

  return (
    <Container maxWidth="sm" sx={{ mt: 5 }}>
      <Paper elevation={3} sx={{ p: 3, borderRadius: 2 }}>
        <Typography variant="h4" sx={{ textAlign: "center", mb: 4, fontWeight: 500 }}>
          Todo App
        </Typography>
        
        <Box sx={{ display: "flex", mb: 3 }}>
          <TextField
            fullWidth
            variant="outlined"
            onChange={onChange}
            value={inputVal}
            placeholder="test"
            size="medium"
            sx={{ mr: 1 }}
            InputProps={{
              sx: { borderRadius: 1 }
            }}
          />
          <Button
            variant="contained"
            onClick={handleClick}
            disabled={!inputVal.trim()}
            sx={{
              height: "56px",
              minWidth: "100px",
              backgroundColor: "#1976d2",
              color: "white",
              fontWeight: "bold",
              borderRadius: 1
            }}
          >
            {isEdited ? "EDIT" : "ADD"}
          </Button>
        </Box>

        <Box sx={{ mt: 3 }}>
          {todos.map((todo) => (
            <Box
              key={todo.id}
              sx={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
                p: 2,
                mb: 1.5,
                backgroundColor: todo.isDone ? "#f5f5f5" : "white",
                borderRadius: 1,
                boxShadow: 1
              }}
            >
              <Box sx={{ display: "flex", alignItems: "center", flex: 1 }}>
                <IconButton onClick={() => handleDone(todo.id)} sx={{ mr: 1 }}>
                  {todo.isDone ? 
                    <CheckCircleOutlineIcon sx={{ color: "green" }} /> : 
                    <RadioButtonUncheckedIcon sx={{ color: "#aaa" }} />
                  }
                </IconButton>
                <Typography 
                  sx={{ 
                    textDecoration: todo.isDone ? "line-through" : "none",
                    color: todo.isDone ? "#888" : "black"
                  }}
                >
                  {todo.val}
                </Typography>
              </Box>
              <Box>
                <IconButton onClick={() => handleEdit(todo.id)} sx={{ color: "#555" }}>
                  <EditIcon fontSize="small" />
                </IconButton>
                <IconButton onClick={() => handleDelete(todo.id)} sx={{ color: "#d32f2f" }}>
                  <DeleteIcon fontSize="small" />
                </IconButton>
              </Box>
            </Box>
          ))}
        </Box>
      </Paper>
    </Container>
  );
}

export default App;