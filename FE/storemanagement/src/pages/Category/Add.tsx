import React, { useEffect, useState } from 'react';
import { Button, Modal, Form, Input, Select, message, DatePicker } from 'antd';
import axios from '../../common/baseAxios';
import CreateCategoryDto from './models/CreateCategoryDto';
interface IOpenModalProps {
    open: boolean,
    closeForm: Function,
}

const AddModal: React.FC<IOpenModalProps> = (props: IOpenModalProps) => {
    const { open, closeForm } = props;
    const onFinish = (input: CreateCategoryDto) => {
        axios.post(`Categories`, input).then((res) => {
            let { data } = res;
            if (data && data.success === true) {
                message.success('Tạo thể loại sản phẩm thành công.')
                closeForm(true);
            }
            else {
                message.error(data.error)
            }
        })
    }
    return <>
        <Modal
            title="THÊM THỂ LOẠI SẢN PHẨM"
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

export default AddModal;