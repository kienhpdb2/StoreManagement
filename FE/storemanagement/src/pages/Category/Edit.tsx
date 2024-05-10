import React, { useEffect, useState } from 'react';
import { Button, Modal, Form, Input, Select, message, DatePicker } from 'antd';
import axios from '../../common/baseAxios';
import EditCategoryDto from './models/EditCategoryDto';
import CategoryDto from './models/CategoryDto';
interface IOpenModalProps {
    open: boolean,
    closeForm: Function,
    category: CategoryDto
}

const EditModal: React.FC<IOpenModalProps> = (props: IOpenModalProps) => {
    const { open, closeForm, category } = props;
    const onFinish = (input: EditCategoryDto) => {
        axios.put(`Categories/${category.id}`, input).then((res) => {
            let { data } = res;
            if (data && data.success === true) {
                message.success('Cập nhật thể loại sản phẩm thành công.')
                closeForm(true);
            }
            else {
                message.error(data.error)
            }
        })
    }
    return <>
        <Modal
            title="CẬP NHẬT THỂ LOẠI SẢN PHẨM"
            centered
            open={open}
            onOk={() => closeForm(false)}
            onCancel={() => closeForm(false)}
            footer={null}
            width={1000}
        >
            <Form
                name="basic"
                layout={'vertical'}
                style={{ maxWidth: 1000 }}
                onFinish={onFinish}
                autoComplete="off"
                initialValues={{
                    ["name"]: category?.name,
                    ["description"]: category?.description
                }}
            >
                <Form.Item label="Tên thể loại" name="name" rules={[{ required: true, message: 'Vui lòng nhập tên thể loại!' }]}>
                    <Input placeholder='Tên thể loại' />
                </Form.Item>
                <Form.Item
                    label="Mô tả"
                    name="description"
                >
                    <Input placeholder='Mô tả' />
                </Form.Item>
                <Form.Item wrapperCol={{ span: 24 }} style={{ textAlign: 'right' }}>
                    <Button type="primary" htmlType="submit" >
                        Lưu
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    </>
}

export default EditModal;